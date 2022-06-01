using System;
using CrazyShooter.Core;
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
        [SerializeField] private PlayerView player;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private PlayerStats playerStats;
        public PlayerController PlayerController => playerController;
        public InteractionsController InteractionsController => interactionsController;
        public PlayerStats PlayerStats => playerStats;
        public PlayerView Player => player;
        public LayerMask PlayerLayer => playerLayer;
        public LayerMask EnemyLayer => enemyLayer;
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
}
