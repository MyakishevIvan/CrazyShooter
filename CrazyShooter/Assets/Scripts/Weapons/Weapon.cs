using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enum;
using Enums;
using UnityEngine;

namespace CrazyShooter.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;
        public WeaponType WeaponType => weaponType;

        public abstract void Init();
    }
}
