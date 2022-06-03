using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enum;
using CrazyShooter.Enemies;
using CrazyShooter.Enums;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyData> enemiesDataList;
        [SerializeField] private List<EnemyStats> enemiesStatsList;
        [SerializeField] private List<EnemyStats> enemiesBossStatsList;
        private Dictionary<EnemyType, Enemy> _enemyDataDict;
        private Dictionary<DifficultyType, EnemyStats> _enemyStatsDict;
        private Dictionary<DifficultyType, EnemyStats> _enemyBossStatsDict;
        public Dictionary<EnemyType, Enemy> EnemiesData => _enemyDataDict ?? InitEnemiesDataDictionary();
        public Dictionary<DifficultyType, EnemyStats> EnemyStats => _enemyStatsDict ?? InitEnemiesStatsDictionary();
        public Dictionary<DifficultyType, EnemyStats> EnemyBossStats => _enemyStatsDict ?? InitEnemiesBossStatsDictionary();

        private Dictionary<DifficultyType, EnemyStats> InitEnemiesStatsDictionary()
        {
            _enemyStatsDict = new Dictionary<DifficultyType, EnemyStats>();
            foreach (var enemyStats in enemiesStatsList)
                _enemyStatsDict.Add(enemyStats.difficultyType, enemyStats);

            return _enemyStatsDict;
        }
        
        private Dictionary<DifficultyType, EnemyStats> InitEnemiesBossStatsDictionary()
        {
            _enemyBossStatsDict = new Dictionary<DifficultyType, EnemyStats>();
            foreach (var enemyStats in enemiesBossStatsList)
                _enemyBossStatsDict.Add(enemyStats.difficultyType, enemyStats);

            return _enemyBossStatsDict;
        }

        //TODO: Возможно переписать
        private Dictionary<EnemyType, Enemy> InitEnemiesDataDictionary()
        {
            _enemyDataDict = new Dictionary<EnemyType, Enemy>();
            foreach (var enemy in enemiesDataList)
                _enemyDataDict.Add(enemy.enemyType, enemy.enemy);

            return _enemyDataDict;
        }
    }

    [Serializable]
    public class EnemyData
    {
        public Enemy enemy;
        public EnemyType enemyType;
    }

    [Serializable]
    public class EnemyStats
    {
        public DifficultyType difficultyType;
        public int runSpeed;
        public int hp;
        public int damage;
    }
}