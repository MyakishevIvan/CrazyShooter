using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Configs;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "BalanceStorage", menuName = "Storages/BalanceStorage")]
    public class BalanceStorage : ScriptableObject
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private WeaponsConfig weaponsConfig;
        [SerializeField] private RoomsConfig roomsConfig;
        [SerializeField] private EnemiesConfig enemiesConfig;
        public PlayerConfig PlayerConfig => playerConfig;
        public WeaponsConfig WeaponsConfig => weaponsConfig;
        public RoomsConfig RoomsConfig => roomsConfig;
        public EnemiesConfig EnemiesConfig => enemiesConfig;
    }
}
