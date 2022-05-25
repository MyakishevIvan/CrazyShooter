using CrayzShooter.Core;
using UnityEngine;

namespace CrayzShooter.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Player player;
        [SerializeField] private float playerSpeed;
        public PlayerController PlayerController => playerController;
        public Player Player => player;
        public float PlayerSpeed => playerSpeed;
    }
}
