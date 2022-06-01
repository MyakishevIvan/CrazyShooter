using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "SwordStats", menuName = "Configs/SwordStats")]
    public class SwordStats : WeaponStats
    {
        [SerializeField] private float speed;
        [SerializeField] private float timeBtwAttack;
        [SerializeField] private int angle;

        public float Speed => speed;
        public int Angle => angle;
        public float TimeBtwAttack => timeBtwAttack;

    }
}
