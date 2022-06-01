using System;
using System.Collections.Generic;
using CrazyShooter.Core;
using CrazyShooter.Enum;
using CrazyShooter.Configs;
using CrazyShooter.Enemies;
using CrazyShooter.Enums;
using CrazyShooter.Interactions;
using CrazyShooter.Rooms;
using Enums;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CrazyShooter.FightScene
{
    public class FightSceneController : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private BalanceStorage _balanceStorage;
        private CameraController.CameraController _cameraController;
        private PlayerView _playerView;
        private EnemyStats _enemyStats;

        private PlayerConfig PlayerConfig => _balanceStorage.PlayerConfig;
        private WeaponsConfig WeaponsConfig => _balanceStorage.WeaponsConfig;
        private EnemiesConfig EnemiesConfig => _balanceStorage.EnemiesConfig;
        private Dictionary<RoomType, BaseRoom> RoomsDict => _balanceStorage.RoomsConfig.RoomsDict;

        private void Awake()
        {
            InitStats();
            InitSpawnRoom();
            InitPlayer();
            
            _cameraController = GetComponent<CameraController.CameraController>();
            _cameraController.Player = _playerView;
        }

        private void InitStats()
        {
            _enemyStats = _balanceStorage.EnemiesConfig.EnemyStats[DifficultyType.Low];
        }

        private void InitSpawnRoom()
        {
            var roomsData = _balanceStorage.RoomsConfig.RoomsData;
            BaseRoom currentRoom = null;

            if (roomsData.roomType == RoomType.SpawnRoom)
            {
                currentRoom =
                    _diContainer.InstantiatePrefabForComponent<SpawnRoom>(RoomsDict[roomsData.roomType], transform);

                InitBorder(currentRoom, roomsData.BorderStates);
            }
            else
            {
                Debug.LogError("First room is not Spawn Room!");
            }

            if (roomsData.dependentRooms.Count == 0)
                return;

            InitDependentRooms(roomsData.dependentRooms, currentRoom);
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
                var gun = WeaponsConfig.GetWeapon(enemy.weaponType);
                var currentEnemy = EnemiesConfig.EnemiesData[enemy.enemyType];

                switch (enemy.enemyType)
                {
                    case EnemyType.melee:
                        instantiatedEnemy =
                            _diContainer.InstantiatePrefabForComponent<MeleeEnemy>(currentEnemy, room.transform);
                        break;
                    case EnemyType.shooter:
                        instantiatedEnemy =
                            _diContainer.InstantiatePrefabForComponent<ShootingEnemy>(currentEnemy, room.transform);
                        break;
                    default:
                        throw new Exception("There is no case for " + enemy.enemyType);
                        break;
                }

                ChangeEnemyPosition(room, instantiatedEnemy);
                instantiatedEnemy.InitEnemy(gun, _enemyStats);
                enemiesList.Add(instantiatedEnemy);
            }

            return enemiesList;
        }

        private void ChangeEnemyPosition(BaseRoom room, Enemy enemy)
        {
            var enemyPos = enemy.transform.position;
            var roomSize = room.Plane.GetComponent<SpriteRenderer>().size;
            var xPos = Random.Range(enemyPos.x - roomSize.x / 2, enemyPos.x + roomSize.x / 2);
            var yPos = Random.Range(enemyPos.y - roomSize.y / 2, enemyPos.y + roomSize.y / 2);
            enemy.transform.position = new Vector3(xPos, yPos, 0);
        }

        private void InitDependentRooms(List<RoomData> dependentRooms, BaseRoom previouseRoom)
        {
            foreach (var room in dependentRooms)
            {
                var instantiatedRoom =
                    _diContainer.InstantiatePrefabForComponent<EnemyRoom>(RoomsDict[room.roomType], transform);

                var pos = previouseRoom.GetRoomPosition(instantiatedRoom, room.directionType);
                instantiatedRoom.transform.localPosition = pos;
                InitBorder(instantiatedRoom, room.BorderStates);

                if (room.dependentRooms.Count > 0)
                    InitDependentRooms(room.dependentRooms, instantiatedRoom);

                if (room.EnemyInRooms.Count > 0)
                {
                    var enemyInRoom = InitEnemies(instantiatedRoom, room.EnemyInRooms);
                    instantiatedRoom.EnemiesInRoom = enemyInRoom;
                }
            }
        }

        private void InitPlayer()
        {
            _playerView = _diContainer.InstantiatePrefabForComponent<PlayerView>(PlayerConfig.Player);

            var playerController =
                _diContainer.InstantiatePrefabForComponent<PlayerController>(PlayerConfig.PlayerController,
                    _playerView.transform);
            _diContainer.InstantiatePrefabForComponent<InteractionsController>(PlayerConfig.InteractionsController,
                _playerView.transform);

            _playerView._playerController = playerController;
            var weapon = _balanceStorage.WeaponsConfig.GetWeapon(WeaponType.Sword);
            _playerView.SetWeapon(ref weapon);
            playerController.SetWeapon(weapon);
        }
    }
}