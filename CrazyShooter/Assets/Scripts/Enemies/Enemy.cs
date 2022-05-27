using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enums;
using UnityEngine;

namespace  CrazyShooter.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;
        public int Hp { get; set; }
    }
    
}
