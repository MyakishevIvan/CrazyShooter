using CrayzShooter.Configs;
using CrayzShooter.Core;
using UnityEngine;
using Zenject;

namespace CrayzShooter.FightScene
{
    public class FightSceneController : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private BalanceStorage _balanceStorage;

        private PlayerConfig _playerConfig => _balanceStorage.PlayerConfig;

        private void Awake()
        {
            var player = _diContainer.InstantiatePrefabForComponent<PlayerView>(_playerConfig.Player);
            var playerController = _diContainer.InstantiatePrefabForComponent<PlayerController>(_playerConfig.PlayerController, player.transform);
            var weapon = _balanceStorage.WeaponsConfig.GetWeapon(Enum.WeaponType.Gun);
            player.InitWeapon(weapon);
            playerController.Speed = _playerConfig.PlayerSpeed;
        }
    }
}
