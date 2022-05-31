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
        protected CharacterType _characterType;
        public float Damage { get; set; }
        public WeaponType WeaponType => weaponType;

        public abstract void Init(CharacterType characterType, Joystick weaponJoystick = null);
    }
}
