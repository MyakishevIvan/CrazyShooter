using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrayzShooter.Configs
{
    [CreateAssetMenu(fileName = "SwordParams", menuName = "Configs/SwordParams")]
    public class SwordParams : ScriptableObject
    {
        [SerializeField] private float damage;
        [SerializeField] private float speed;

        public float Damage => damage;
        public float Speed => speed;

    }
}
