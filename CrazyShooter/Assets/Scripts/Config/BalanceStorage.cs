using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrayzShooter.Configs
{
    [CreateAssetMenu(fileName = "BalanceStorage", menuName = "Storages/BalanceStorage")]
    public class BalanceStorage : ScriptableObject
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private WeaponsConfig waponsConfig;
        public PlayerConfig PlayerConfig => playerConfig;
        public WeaponsConfig WeaponsConfig => waponsConfig;
    }
}
