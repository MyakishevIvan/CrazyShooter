using System.Collections;
using CrazyShooter.Configs;
using CrazyShooter.Enums;
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
        private SwordStats _swordStats;
        private const float _delay = 0.5f;
        private int _startAngle;
        private CharacterType _weaponOwner;
        public bool IsAttacking { get; private set; }

        private SwordStats SwordStats => (SwordStats)_balance.WeaponsConfig.WeaponsDataDict[WeaponType].WeaponStats;
        private int angleIndex => transform.lossyScale.x >= 0 ? 1 : -1;
        private int _totalDamage;
        private void Start()
        {
           transform.rotation = Quaternion.Euler(0f,0f,0f);
        }

        public override void Init(WeaponStats weaponStats, int characterDamage, CharacterType weaponOwner)
        {
            _swordStats = (SwordStats)weaponStats;
            _totalDamage = (int)_swordStats.Damage + characterDamage;
            _weaponOwner = weaponOwner;
        }
        
        private void MakeDamage()
        {
            if (_weaponOwner == CharacterType.PLayer)
            {
                var enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange,
                    _balance.PlayerConfig.EnemyLayer);

                foreach (var enemy in enemies)
                {
                    Debug.LogError("Sword Enemy hitted damage : " + _totalDamage);
                }
            }
            
            if (_weaponOwner == CharacterType.Enemy)
            {
                var player = Physics2D.OverlapCircleAll(attackPos.position, attackRange,
                    _balance.PlayerConfig.PlayerLayer);

               if(player.Length != 0)
                    Debug.LogError("Sword Player hitted damage : " + _totalDamage);
                
            }
           
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
                        Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, index * _swordStats.Angle), Time.deltaTime * _swordStats.Speed);

                    var isEndAngle = false;
                    var endAngle = 0;
                    
                    if (angleIndex > 0)
                    {
                        endAngle = 360 + _swordStats.Angle;
                        isEndAngle = (int)transform.eulerAngles.z <= endAngle;
                    }
                    else
                    {
                        endAngle = -_swordStats.Angle;
                        isEndAngle = transform.eulerAngles.z >= endAngle -1;
                    }
                    
                    if (isEndAngle)
                    {
                        MakeDamage();
                        swordDown = true;
                    }
                }
                else
                {
                    transform.rotation =
                        Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, _startAngle),
                            Time.deltaTime * _swordStats.Speed);

                    if (transform.eulerAngles.z >= _startAngle)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, _startAngle);
                        Invoke("DelayAttack", _delay);
                        StopAllCoroutines();
                    }
                }

                yield return null;
            }
        }

        private void DelayAttack() => IsAttacking = false;
        
        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}