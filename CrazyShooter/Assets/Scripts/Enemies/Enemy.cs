using CrazyShooter.Configs;
using CrazyShooter.Core;
using CrazyShooter.Enums;
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
        [Inject] DiContainer _diContainer;
        private int _hp;
        protected Weapon Weapon { get; private set; }
        protected bool IsAttacking { get; set;}
        protected EnemyStats EnemyStats { get; set;}

        public virtual void InitEnemy(WeaponData weaponData, EnemyStats stats)
        {
            var currentWeapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weaponData.Weapon, weaponTarget);
            currentWeapon.Init(weaponData.WeaponStats, stats.damage, CharacterType.Enemy);
            currentWeapon.GetComponent<SpriteRenderer>().sortingLayerName = "EnemyWeapon";
            Weapon = currentWeapon;
            EnemyStats = stats;
            _hp = EnemyStats.hp;
        }

        public virtual void TakeDamage(int damage)
        {
            _hp -= damage;
            Debug.LogError("Enemy Hp " + _hp);
            
            if(_hp <= 0)
                Destroy(gameObject);
        }

        public abstract void Attack(PlayerView player);
        public abstract void StopAttack();
    }
    
}
