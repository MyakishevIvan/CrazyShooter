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
        [SerializeField] private List<EnemyData> enemyData;
        private Dictionary<EnemyType, Enemy> _enemyDict;
        public Dictionary<EnemyType, Enemy> EnemyData => _enemyDict ?? InitEnemiesDictionary();

        //TODO: Возможно переписать
        private Dictionary<EnemyType, Enemy> InitEnemiesDictionary()
        {
            _enemyDict = new Dictionary<EnemyType, Enemy>();
            foreach (var enemy in enemyData)
                _enemyDict.Add(enemy.enemyType, enemy.enemy);

            return _enemyDict;
        }
    }

    [Serializable]
    public class EnemyData
    {
        public Enemy enemy;
        public EnemyType enemyType;
    }
}