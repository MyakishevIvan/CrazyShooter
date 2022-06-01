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
        protected Weapon Weapon { get; private set; }
        protected bool IsAttacking { get; set;}
        protected EnemyStats EnemyStats { get; set;}

        public virtual void InitEnemy(Weapon weapon, EnemyStats stats)
        {
            var currentWeapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weapon.gameObject, weaponTarget);
            currentWeapon.Init();
            currentWeapon.GetComponent<SpriteRenderer>().sortingLayerName = "EnemyWeapon";
            Weapon = currentWeapon;
            EnemyStats = stats;
        }

        public abstract void Attack(PlayerView player);
        public abstract void StopAttack();
    }
    
}
