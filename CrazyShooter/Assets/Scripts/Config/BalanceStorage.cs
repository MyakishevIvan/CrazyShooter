using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrazyShooter.Configs;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "BalanceStorage", menuName = "Storages/BalanceStorage")]
    public class BalanceStorage : ScriptableObject
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private WeaponsConfig weaponsConfig;
        [SerializeField] private MapsConfig mapsConfig;
        [SerializeField] private EnemiesConfig enemiesConfig;
        [SerializeField] private SpriteConfig spriteConfig;
        public PlayerConfig PlayerConfig => playerConfig;
        public WeaponsConfig WeaponsConfig => weaponsConfig;
        public EnemiesConfig EnemiesConfig => enemiesConfig;
        public SpriteConfig SpriteConfig => spriteConfig;
        public MapsConfig MapsConfig => mapsConfig;
    }

  
}
