using CrayzShooter.Core;
using UnityEngine;

namespace CrayzShooter.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerView player;
        [SerializeField] private float playerSpeed;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask playerLayer;
        public PlayerController PlayerController => playerController;
        public PlayerView Player => player;
        public float PlayerSpeed => playerSpeed;
        public LayerMask PlayerLayer => playerLayer;
        public LayerMask EnemyLayer => enemyLayer;
    }
}
