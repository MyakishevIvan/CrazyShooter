using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "SwordParams", menuName = "Configs/SwordParams")]
    public class SwordParams : ScriptableObject
    {
        [SerializeField] private float damage;
        [SerializeField] private float speed;
        [SerializeField] private float timeBtwAttack;
        [SerializeField] private int angle;

        public float Damage => damage;
        public float Speed => speed;
        public int Angle => angle;
        public float TimeBtwAttack => timeBtwAttack;

    }
}
