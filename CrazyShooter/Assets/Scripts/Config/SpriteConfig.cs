using System;
using System.Collections.Generic;
using CrazyShooter.Enum;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "SpriteConfig", menuName = "Configs/SpriteConfig")]
    public class SpriteConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponSpriteConfig> weaponSpriteConfigs;

        public Sprite GetWeaponSprite(WeaponType weaponType)
        {
            Sprite result = null;
            foreach (var weaponSpriteConfig in weaponSpriteConfigs)
            {
                if (weaponSpriteConfig.WeaponType == weaponType)
                    result = weaponSpriteConfig.sprite;
            }

            if (result != null)
                return result;
            else
                throw new Exception("There is no sprite for type " + weaponType);
        }
    }

    [Serializable]
    public class WeaponSpriteConfig
    {
        public Sprite sprite;
        public WeaponType WeaponType;
    }
}