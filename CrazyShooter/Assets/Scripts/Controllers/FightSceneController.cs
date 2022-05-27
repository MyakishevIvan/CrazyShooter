using CrayzShooter.Configs;
using CrayzShooter.Core;
using CrayzShooter.Enum;
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
            var player = _diContainer.InstantiatePrefabForComponent<PlayerView>(_playerConfig.Player);
            var playerController = _diContainer.InstantiatePrefabForComponent<PlayerController>(_playerConfig.PlayerController, player.transform);
            playerController.UpdageControllerView(_weaponType);
            player._playerController = playerController;
            var weapon = _balanceStorage.WeaponsConfig.GetWeapon(_weaponType);
            player.InitWeapon(weapon, playerController.ShootJoystick);
            playerController.Speed = _playerConfig.PlayerSpeed;
        }
    }
}
