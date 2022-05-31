using System.Collections;
using CrazyShooter.Configs;
using Enums;
using UnityEngine;
using Zenject;

namespace CrazyShooter.Weapons
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
        public bool IsAttacking { get; set; }

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
        
        private void DamageEnemy()
        {
            var enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange,
               _balance.PlayerConfig.EnemyLayer);
           
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }

        public void StartAttack()
        {
            _startAngle = 0;
            IsAttacking = true;
            StopAllCoroutines();
            StartCoroutine(Attack());
        }
        private IEnumerator Attack()
        {
            var swordDown = false;
            var index = angleIndex;

            while (IsAttacking)
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

                    if (transform.eulerAngles.z >= _startAngle)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, _startAngle);
                        IsAttacking = false;
                    }
                }

                yield return null;
            }
        }
        
        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}