using CrayzShooter.Configs;
using CrayzShooter.Core;
using CrayzShooter.Enum;
using CrazyShooter.Enums;
using CrazyShooter.Rooms;
using UnityEngine;
using Zenject;

namespace CrayzShooter.FightScene
{
    public class FightSceneController : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private BalanceStorage _balanceStorage;
        private WeaponType _weaponType = WeaponType.Gun;

        private PlayerConfig _playerConfig => _balanceStorage.PlayerConfig;

        private void Awake()
        {
            InitRooms();
            InitPlayer();
        }

        private void InitPlayer()
        {
            var player = _diContainer.InstantiatePrefabForComponent<PlayerView>(_playerConfig.Player);
            var playerController =
                _diContainer.InstantiatePrefabForComponent<PlayerController>(_playerConfig.PlayerController, player.transform);
            playerController.UpdageControllerView(_weaponType);
            player._playerController = playerController;
            var weapon = _balanceStorage.WeaponsConfig.GetWeapon(_weaponType);
            player.InitWeapon(weapon, playerController.ShootJoystick);
        }

        private void InitRooms()
        {
            var rooms = _balanceStorage.RoomsConfig.Rooms;

            BaseRoom previouseRoom = null;
            foreach (var room in rooms)
            {
                if (room.RoomType == RoomType.SpawnRoom)
                {
                    previouseRoom = _diContainer.InstantiatePrefabForComponent<SpawnRoom>(room, transform);
                }
                else
                {
                    var instantiatedRoom = _diContainer.InstantiatePrefabForComponent<EnemyRoom>(room,transform);
                    var pos = previouseRoom.GetRoomPosition(instantiatedRoom, RoomDirectionType.Bottom);
                    instantiatedRoom.transform.localPosition = pos;
                }
                    
            }
        }
    }
}
