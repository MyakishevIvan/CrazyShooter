using System;
using System.Collections;
using System.Collections.Generic;
using CrayzShooter.Enum;
using CrazyShooter.Enemies;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyData> enemyData;
        
        public List<EnemyData> EnemyData => EnemyData;
    }

    [Serializable]
    public class EnemyData
    {
        public Enemy enemy;
        public int hp;
        public WeaponType weaponType;
    }
}