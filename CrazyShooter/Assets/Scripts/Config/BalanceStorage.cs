using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrayzShooter.Configs
{
    [CreateAssetMenu(fileName = "BalanceStorage", menuName = "Storages/BalanceStorage")]
    public class BalanceStorage : ScriptableObject
    {
        [SerializeField] private PlayerConfig playerConfig;

        public PlayerConfig PlayerConfig => playerConfig;
    }
}
