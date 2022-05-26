using CrayzShooter.Core;
using UnityEngine;

namespace CrayzShooter.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerView player;
        [SerializeField] private float playerSpeed;
        public PlayerController PlayerController => playerController;
        public PlayerView Player => player;
        public float PlayerSpeed => playerSpeed;
    }
}
