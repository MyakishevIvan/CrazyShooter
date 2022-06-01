using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Configs;
using CrazyShooter.Enum;
using CrazyShooter.Enums;
using Enums;
using UnityEngine;

namespace CrazyShooter.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;
        public WeaponType WeaponType => weaponType;
        protected WeaponStats _weaponStats;

        public abstract void Init(WeaponStats weaponStats, int characterDamage, CharacterType weaponOwner);
    }
}
