using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enum;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "GunStats", menuName = "Configs/GunStats")]
    public class GunStats : WeaponStats
    {
        [SerializeField] private float bulletspeed;
        [SerializeField] private float reloadTime;

        public float Bulletspeed => bulletspeed;
        public float ReloadTime => reloadTime;
    }
}
