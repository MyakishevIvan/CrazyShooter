using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enum;
using UnityEngine;

namespace CrazyShooter.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;
        public float Damage { get; set; }
        public WeaponType WeaponType => weaponType;

        public abstract void Init(Joystick weaponJoystick = null);
    }
}
