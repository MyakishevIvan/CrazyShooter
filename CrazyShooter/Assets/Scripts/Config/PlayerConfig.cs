using System;
using System.Collections.Generic;
using CrazyShooter.Core;
using CrazyShooter.Enums;
using CrazyShooter.Interactions;
using DG.Tweening.Plugins;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private InteractionsController interactionsController;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private List<PlayerData> playerDataList;
        
        private Dictionary<PlayerType, PlayerData> _playerDataDict;
        public Dictionary<PlayerType, PlayerData> PlayerData => _playerDataDict ?? CreatPlayerDict();

        public PlayerController PlayerController => playerController;
        public InteractionsController InteractionsController => interactionsController;
        public LayerMask PlayerLayer => playerLayer;
        public LayerMask EnemyLayer => enemyLayer;
        
        private Dictionary<PlayerType, PlayerData> CreatPlayerDict()
        {
            _playerDataDict = new Dictionary<PlayerType, PlayerData>();
            
            foreach (var playerData in playerDataList)
                _playerDataDict.Add(playerData.PlayerType, playerData);

            return _playerDataDict;
        }

    }

    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private float playerSpeed;
        [SerializeField] private int hp;
        [SerializeField] private int damage;
        public float PlayerSpeed => playerSpeed;
        public int Hp => hp;
        public int Damage => damage;

    }

    [Serializable]
    public class PlayerData
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerStats playerStats;

        public PlayerView PlayerView => playerView;
        public PlayerStats PlayerStats => playerStats;
        public PlayerType PlayerType => PlayerView.PlayerType;
    }
}
