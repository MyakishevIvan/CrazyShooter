using CrayzShooter.Enum;
using CrayzShooter.Weapons;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CrayzShooter.Configs
{
    [CreateAssetMenu(fileName ="WeaponsConfig", menuName = "Configs/WeaponsConfig")]
    public class WeaponsConfig : ScriptableObject
    {
        [SerializeField] private List<Weapon> weapons;
        [SerializeField] private GunParams gunParams;
        [SerializeField] private SwordParams swordParams;

        public GunParams GunParams => gunParams;
        public SwordParams SwordParams => swordParams;

        public Weapon GetWeapon(WeaponType weaponType)
        {
            Weapon result = null;
            var weapon = weapons.First(x => x.WeaponType == weaponType);
            switch (weaponType)
            {
                case WeaponType.Sword:
                    result = weapon as Sword;
                    break;
                case WeaponType.Gun:
                    result = weapon as Gun;
                    break;
                default:
                    Debug.LogError("There is no case for Wapontype " + weaponType);
                    break;
            }

            return result;
        }
    }
}