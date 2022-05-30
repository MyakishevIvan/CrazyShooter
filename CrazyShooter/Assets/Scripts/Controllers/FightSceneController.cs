using System.Collections.Generic;
using CrazyShooter.Core;
using CrazyShooter.Enum;
using CrazyShooter.Configs;
using CrazyShooter.Enums;
using CrazyShooter.Rooms;
using UnityEngine;
using Zenject;

namespace CrazyShooter.FightScene
{
    public class FightSceneController : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private BalanceStorage _balanceStorage;
        private WeaponType _weaponType = WeaponType.Gun;
        private CameraController.CameraController _cameraController;
        private PlayerView _playerView;

        private PlayerConfig PlayerConfig => _balanceStorage.PlayerConfig;
        private Dictionary<RoomType, BaseRoom> RoomsDict => _balanceStorage.RoomsConfig.RoomsDict;
        private void Awake()
        {
            InitSpawnRoom();
            InitPlayer();
            _cameraController = GetComponent<CameraController.CameraController>();
            _cameraController.Player = _playerView;
        }


        private void InitSpawnRoom(RoomData roomsData = null )
        {
            roomsData ??= _balanceStorage.RoomsConfig.RoomsData;
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
            if(borderStates.Count == 0)
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

        private void InitDependentRooms(List<RoomData> dependentRooms, BaseRoom previouseRoom)
        {
            foreach (var room in dependentRooms)
            {
                var instantiatedRoom =
                    _diContainer.InstantiatePrefabForComponent<EnemyRoom>(RoomsDict[room.roomType], transform);
                var pos = previouseRoom.GetRoomPosition(instantiatedRoom, room.directionType);
                instantiatedRoom.transform.localPosition = pos;
                InitBorder(instantiatedRoom, room.BorderStates);
                
                if (room.dependentRooms.Count == 0)
                    continue;
            
                InitDependentRooms(room.dependentRooms, instantiatedRoom);
            }
        }
        
        private void InitPlayer()
        {
            _playerView = _diContainer.InstantiatePrefabForComponent<PlayerView>(PlayerConfig.Player);
            var playerController =
                _diContainer.InstantiatePrefabForComponent<PlayerController>(PlayerConfig.PlayerController,
                    _playerView.transform);
            playerController.UpdageControllerView(_weaponType);
            _playerView._playerController = playerController;
            var weapon = _balanceStorage.WeaponsConfig.GetWeapon(_weaponType);
            _playerView.InitWeapon(weapon, playerController.ShootJoystick);
        }
    }
}
