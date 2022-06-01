using CrazyShooter.Enum;
using UnityEngine;

namespace CrazyShooter.Configs
{
    public class WeaponStats: ScriptableObject
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private float damage;
        
        public WeaponType WeaponType => weaponType;
        public float Damage => damage;


    }
}