using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "GunParams", menuName = "Configs/GunParams")]
    public class GunParams : ScriptableObject
    {
        [SerializeField] private float damage;
        [SerializeField] private float bulletspeed;
        [SerializeField] private float reloadTime;

        public float Damage => damage;
        public float Bulletspeed => bulletspeed;
        public float ReloadTime => reloadTime;
    }
}
