using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using CrazyShooter.Core;
using CrazyShooter.Enum;
using CrazyShooter.Configs;
using CrazyShooter.Enemies;
using CrazyShooter.Enums;
using CrazyShooter.Progressbar;
using CrazyShooter.Rooms;
using CrazyShooter.Signals;
using CrazyShooter.System;
using CrazyShooter.Windows;
using SMC.Windows;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CrazyShooter.FightScene
{
    public class FightSceneController : MonoBehaviour
    {
        [SerializeField] private GameObject progressbarContainer;
        [Inject] private DiContainer _diContainer;
        [Inject] private BalanceStorage _balanceStorage;
        [Inject] private SceneTransitionSystem _sceneTransitionSystem;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private GameModel _gameModel;
        [Inject] private PlayerManager _playerManager;
        [Inject] private SignalBus _signalBus;
        private CameraController.CameraController _cameraController;
        private EnemyStats _enemyStats;
        private EnemyStats _bossStats;
        private PlayerData _playerData;
        private WeaponData _playerWeaponData;
        private PlayerView _playerView;
        private RoomsConfig _roomsConfig;
        private MapData _mapData;
        private int _enemyCount;

        private WeaponsConfig WeaponsConfig => _balanceStorage.WeaponsConfig;
        private PlayerConfig PlayerConfig => _balanceStorage.PlayerConfig;
        private EnemiesConfig EnemiesConfig => _balanceStorage.EnemiesConfig;
        

        private void Awake()
        {
            InitBalance();
            InitSpawnRoom();
            InitPlayer();
            
            _cameraController = GetComponent<CameraController.CameraController>();
            _cameraController.Player = _playerView;
        }

        private void InitBalance()
        {
            _mapData = _balanceStorage.MapsConfig.GetRoomsConfig(_gameModel.CurrnentMapLevel);
            _enemyStats = _balanceStorage.EnemiesConfig.EnemyStats[_mapData.enemyDifficalt];
            _bossStats = _balanceStorage.EnemiesConfig.EnemyBossStats[_mapData.bossDifficalt];
            _roomsConfig = _mapData.roomsConfig;
            _playerData = _balanceStorage.PlayerConfig.PlayerData[_gameModel.PlayerType];
            _playerWeaponData = _balanceStorage.WeaponsConfig.WeaponsDataDict[_gameModel.WeaponType];
        }

        private void InitSpawnRoom()
        {
            var roomsData = _roomsConfig.RoomsData;
            var roomsDict = _roomsConfig.RoomsDict;
            BaseRoom currentRoom = null;

            if (roomsData.roomType == RoomType.SpawnRoom)
            {
                currentRoom =
                    _diContainer.InstantiatePrefabForComponent<SpawnRoom>(roomsDict[roomsData.roomType], transform);

                InitBorder(currentRoom, roomsData.BorderStates);
            }
            else
            {
                Debug.LogError("First room is not Spawn Room!");
            }

            if (roomsData.dependentRooms.Count == 0)
                return;

            InitDependentRooms(roomsData.dependentRooms, currentRoom);

            if (_enemyCount == 0)
                throw new Exception("In rooms no implemented enemies");
        }

        private void InitBorder(BaseRoom currentRoom, List<BorderState> borderStates)
        {
            if (borderStates.Count == 0)
                return;

            foreach (var borderState in borderStates)
            {
                switch (borderState.borderSide)
                {
                    case DirectionType.Left:
                        currentRoom.LeftBorder.SetState(borderState.borderType);
                        break;
                    case DirectionType.Right:
                        currentRoom.RightBorder.SetState(borderState.borderType);
                        break;
                    case DirectionType.Top:
                        currentRoom.TopBorder.SetState(borderState.borderType);
                        break;
                    case DirectionType.Bottom:
                        currentRoom.BottomBorder.SetState(borderState.borderType);
                        break;
                    default:
                        Debug.LogError("There is no case for borderSide " + borderState.borderSide);
                        break;
                }
            }
        }

        private List<Enemy> InitEnemies(BaseRoom room, List<EnemyInRoom> enemies)
        {
            var enemiesList = new List<Enemy>();
            Enemy instantiatedEnemy;
            
            foreach (var enemy in enemies)
            {
                var weaponData = WeaponsConfig.WeaponsDataDict[enemy.weaponType];
                var currentEnemy = EnemiesConfig.EnemiesData[enemy.enemyType];

                switch (enemy.enemyType)
                {
                    case EnemyType.melee:
                        instantiatedEnemy =
                            _diContainer.InstantiatePrefabForComponent<MeleeEnemy>(currentEnemy, room.transform);
                        ChangeEnemyPosition(room, instantiatedEnemy);
                        break;
                    case EnemyType.shooter:
                        instantiatedEnemy =
                            _diContainer.InstantiatePrefabForComponent<ShootingEnemy>(currentEnemy, room.transform);
                        ChangeEnemyPosition(room, instantiatedEnemy);
                        break;
                    case EnemyType.meleeBoss:
                        instantiatedEnemy =
                            _diContainer.InstantiatePrefabForComponent<MeleeEnemy>(currentEnemy, room.transform);
                        break;
                    case EnemyType.shooterBoss:
                        instantiatedEnemy =
                            _diContainer.InstantiatePrefabForComponent<ShootingEnemy>(currentEnemy, room.transform);
                        break;
                    default:
                        throw new Exception("There is no case for " + enemy.enemyType);
                }
                
                var hpBarController =  _diContainer.InstantiatePrefabForComponent<HpProgressbarController>(PlayerConfig.HpProgressbarController, 
                    instantiatedEnemy.ProgressbarPos.position, Quaternion.Euler(0,0,90), progressbarContainer.transform);
                
                var stats = enemy.enemyType == EnemyType.melee || enemy.enemyType == EnemyType.shooter
                    ? _enemyStats
                    : _bossStats;
                
                instantiatedEnemy.InitEnemy(weaponData,hpBarController, stats, enemy.enemyType, DecreaseEnemyCount);
            
                hpBarController.Init(CharacterType.Enemy, instantiatedEnemy.ProgressbarPos );
                enemiesList.Add(instantiatedEnemy);
            }

            return enemiesList;
        }

        private void ChangeEnemyPosition(BaseRoom room, Enemy enemy)
        {
            var enemyPos = enemy.transform.position;
            var roomSize = room.Plane.GetComponent<SpriteRenderer>().size;
            var xPos = Random.Range(enemyPos.x - roomSize.x / 4, enemyPos.x + roomSize.x / 4);
            var yPos = Random.Range(enemyPos.y - roomSize.y / 4, enemyPos.y + roomSize.y / 4);
            enemy.transform.position = new Vector3(xPos, yPos, 0);
        }

        private void InitDependentRooms(List<RoomData> dependentRooms, BaseRoom previouseRoom)
        {
            foreach (var room in dependentRooms)
            {
                var instantiatedRoom =
                    _diContainer.InstantiatePrefabForComponent<EnemyRoom>(_roomsConfig.RoomsDict[room.roomType], transform);

                var pos = previouseRoom.GetRoomPosition(instantiatedRoom, room.directionType);
                instantiatedRoom.transform.localPosition = pos;
                InitBorder(instantiatedRoom, room.BorderStates);

                if (room.dependentRooms.Count > 0)
                    InitDependentRooms(room.dependentRooms, instantiatedRoom);

                if (room.EnemyInRooms.Count > 0)
                {
                    var enemyInRoom = InitEnemies(instantiatedRoom, room.EnemyInRooms);
                    instantiatedRoom.EnemiesInRoom = enemyInRoom;
                    _enemyCount += enemyInRoom.Count;
                }
            }
        }

        private void InitPlayer()
        {
            _playerView = _diContainer.InstantiatePrefabForComponent<PlayerView>(_playerData.PlayerView);
            var hpBarController =  _diContainer.InstantiatePrefabForComponent<HpProgressbarController>(PlayerConfig.HpProgressbarController, 
                _playerView.ProgressbarPos.position, Quaternion.Euler(0,0,90), progressbarContainer.transform);
            
            hpBarController.Init(CharacterType.PLayer,  _playerView.ProgressbarPos );
            _playerView.Init(hpBarController, _playerWeaponData, PlayerConfig.PlayerController,
                PlayerConfig.InteractionsController, _playerData.PlayerStats, GoToMenuScene);
        }
        
        private void DecreaseEnemyCount()
        {
            _enemyCount--;
            if (_enemyCount == 0)
            {
                _signalBus.Fire<PlayerWinSignal>();
                _playerManager.IncreaseMapLevel();
                Invoke(nameof(GoToMenuScene), 2f);
            }
        }
        
        private void GoToMenuScene()
        {
            var setup = new FightResultWindowSetup()
            {
                title = _enemyCount == 0? "You Win": "You Die",
                onButtonClick = () => _sceneTransitionSystem.GoToScene(SceneType.Menu, true, true)
            };
            _windowsManager.Open<FightResultWindow, FightResultWindowSetup>(setup);
        }
    }
}