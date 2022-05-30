using System.Collections.Generic;
using CrayzShooter.Configs;
using CrayzShooter.Core;
using CrayzShooter.Enum;
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

        private PlayerConfig PlayerConfig => _balanceStorage.PlayerConfig;
        private Dictionary<RoomType, BaseRoom> RoomsDict => _balanceStorage.RoomsConfig.RoomsDict;
        private void Awake()
        {
            InitSpawnRoom();
            InitPlayer();
        }

        private void InitPlayer()
        {
            var player = _diContainer.InstantiatePrefabForComponent<PlayerView>(PlayerConfig.Player);
            var playerController =
                _diContainer.InstantiatePrefabForComponent<PlayerController>(PlayerConfig.PlayerController,
                    player.transform);
            playerController.UpdageControllerView(_weaponType);
            player._playerController = playerController;
            var weapon = _balanceStorage.WeaponsConfig.GetWeapon(_weaponType);
            player.InitWeapon(weapon, playerController.ShootJoystick);
        }

        private void InitSpawnRoom(RoomData roomsData = null )
        {
            roomsData ??= _balanceStorage.RoomsConfig.RoomsData;
            BaseRoom currentRoom = null;
            
            if (roomsData.roomType == RoomType.SpawnRoom)
            {
                currentRoom =
                    _diContainer.InstantiatePrefabForComponent<SpawnRoom>(RoomsDict[roomsData.roomType], transform);
            }
            else
            {
                Debug.LogError("First room is not Spawn Room!");
            }

            if (roomsData.dependentRooms.Count == 0)
                return;
            
            InitDependentRooms(roomsData.dependentRooms, currentRoom);
        }

        private void InitDependentRooms(List<RoomData> dependentRooms, BaseRoom previouseRoom)
        {
            foreach (var room in dependentRooms)
            {
                var instantiatedRoom =
                    _diContainer.InstantiatePrefabForComponent<EnemyRoom>(RoomsDict[room.roomType], transform);
                var pos = previouseRoom.GetRoomPosition(instantiatedRoom, room.roomDirectionType);
                instantiatedRoom.transform.localPosition = pos;
                
                if (room.dependentRooms.Count == 0)
                    continue;
            
                InitDependentRooms(room.dependentRooms, instantiatedRoom);
            }


            // if (roomData.dependentRooms.Count > 0)
            //     InitDependentRooms(roomData.dependentRooms);
        }
    }
}
