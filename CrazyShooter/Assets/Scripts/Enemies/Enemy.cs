using System;
using CrazyShooter.Configs;
using CrazyShooter.Core;
using CrazyShooter.Enums;
using CrazyShooter.Signals;
using CrazyShooter.Weapons;
using Enums;
using UnityEngine;
using Zenject;

namespace  CrazyShooter.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] private Transform weaponTarget;
        [Inject] private DiContainer _diContainer;
        [Inject] private SignalBus _signalBus;
        private EnemyType _enemyType;
        private int _hp;
        protected Weapon Weapon { get; private set; }
        protected bool IsAttacking { get; set;}
        protected EnemyStats EnemyStats { get; set;}
        
        public virtual void InitEnemy(WeaponData weaponData, EnemyStats stats , EnemyType enemyType)
        {
            var currentWeapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weaponData.Weapon, weaponTarget);
            currentWeapon.Init(weaponData.WeaponStats, stats.damage, CharacterType.Enemy);
            currentWeapon.GetComponent<SpriteRenderer>().sortingLayerName = "EnemyWeapon";
            Weapon = currentWeapon;
            EnemyStats = stats;
            _hp = EnemyStats.hp;
            _enemyType = enemyType;
            _signalBus.Subscribe<PlayerDiedSignal>(DisableObject);
        }

        public virtual void TakeDamage(int damage)
        {
            _hp -= damage;

            if (_hp <= 0)
            {
                _signalBus.Fire(new EnemyDieEffectSignal(transform, _enemyType));
                Destroy(gameObject);
            }
        }

        public abstract void Attack(PlayerView player);
        public abstract void StopAttack();
        
        public void DisableObject()
        {
            StopAllCoroutines();
            this.enabled = false;
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(DisableObject);
        }
    }
    
}
