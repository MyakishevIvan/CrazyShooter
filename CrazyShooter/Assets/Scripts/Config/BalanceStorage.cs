using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Configs;
using UnityEngine;

namespace CrayzShooter.Configs
{
    [CreateAssetMenu(fileName = "BalanceStorage", menuName = "Storages/BalanceStorage")]
    public class BalanceStorage : ScriptableObject
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private WeaponsConfig weaponsConfig;
        [SerializeField] private RoomsConfig roomsConfig;
        public PlayerConfig PlayerConfig => playerConfig;
        public WeaponsConfig WeaponsConfig => weaponsConfig;
        public RoomsConfig RoomsConfig => roomsConfig;
    }
}
