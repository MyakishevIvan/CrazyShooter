using System;
using CrayzShooter.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrayzShooter.Configs;
using UnityEngine;
using Zenject;

namespace CrayzShooter.Weapons
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private Transform attackPos;
        [SerializeField] private float attackRange;
        [Inject] private BalanceStorage _balance;
        private int _angle;
        private float _speed;
        private float _damage;
        private int _startAngle;
        private bool _attacking;

        private SwordParams SwordParams => _balance.WeaponsConfig.SwordParams;
        private int angleIndex => transform.lossyScale.x >= 0 ? 1 : -1;
        private void Start()
        {
           transform.rotation = Quaternion.Euler(0f,0f,0f);
        }

        public override void Init()
        {
            _angle = SwordParams.Angle;
            _speed = SwordParams.Speed;
            _damage = SwordParams.Damage;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && !_attacking)
            {
                _startAngle = 0;
                _attacking = true;
                StopAllCoroutines();
                StartCoroutine(Attack());
            }
        }

        private void DamageEnemy()
        {
            var enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange,
               _balance.PlayerConfig.EnemyLayer);
            for (int i = 0; i < enemies.Length; i++)
            {
                Debug.LogError("Hit");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }

        private IEnumerator Attack()
        {
            var swordDown = false;
            var index = angleIndex;

            while (_attacking)
            {
                if (!swordDown)
                {
                    transform.rotation =
                        Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, index * _angle), Time.deltaTime * _speed);

                    var isEndAngle = false;
                    var endAngle = 0;
                    
                    if (angleIndex > 0)
                    {
                        endAngle = 360 + _angle;
                        isEndAngle = (int)transform.eulerAngles.z <= endAngle;
                    }
                    else
                    {
                        endAngle = -_angle;
                        isEndAngle = transform.eulerAngles.z >= endAngle -1;
                    }
                    

                    if (isEndAngle)
                    {
                        DamageEnemy();
                        swordDown = true;
                    }
                }
                else
                {
                    transform.rotation =
                        Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, _startAngle),
                            Time.deltaTime * _speed);

                    if ((int)transform.eulerAngles.z >= _startAngle)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, _startAngle);
                        _attacking = false;
                    }
                }

                yield return null;
            }
        }
    }
}