using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrazyShooter.Enum;
using CrazyShooter.Weapons;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName ="WeaponsConfig", menuName = "Configs/WeaponsConfig")]
    public class WeaponsConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponData> weaponsDataList;

        private Dictionary<WeaponType, WeaponData> _weaponsDataDict;

        public Dictionary<WeaponType, WeaponData> WeaponsDataDict => _weaponsDataDict ?? CreatWeaponsDataDictionary();
        
        private Dictionary<WeaponType, WeaponData> CreatWeaponsDataDictionary()
        {
            _weaponsDataDict = new Dictionary<WeaponType, WeaponData>();
            foreach (var weapon in weaponsDataList)
                _weaponsDataDict.Add(weapon.WeaponType, weapon);

            return _weaponsDataDict;
        }
    }

    [Serializable]
    public class WeaponData
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponStats weaponStats;

        public Weapon Weapon => weapon;
        public WeaponStats WeaponStats => weaponStats;
        public WeaponType WeaponType => weapon.WeaponType;
    }
}
