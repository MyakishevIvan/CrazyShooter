using System;
using CrazyShooter.Configs;
using CrazyShooter.Core;
using CrazyShooter.Enums;
using CrazyShooter.Progressbar;
using CrazyShooter.Signals;
using CrazyShooter.Weapons;
using Enums;
using UnityEngine;
using Zenject;

namespace CrazyShooter.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] private Transform weaponTarget;
        [SerializeField] private SpriteRenderer head;
        [SerializeField] private Transform progressBarPos;
        [Inject] private DiContainer _diContainer;
        [Inject] private SignalBus _signalBus;
        private EnemyType _enemyType;
        private HpProgressbarController _progressbarController;
        private int _hp;
        private Action OnDieAction;
        private readonly Color _hitTintColor = Color.red;
        protected Weapon Weapon { get; private set; }
        protected bool IsAttacking { get; set; }
        protected EnemyStats EnemyStats { get; set; }
        public Transform ProgressbarPos => progressBarPos;

        public virtual void InitEnemy(WeaponData weaponData, HpProgressbarController progressbarController,
            EnemyStats stats, EnemyType enemyType, Action onDieAction)
        {
            var currentWeapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weaponData.Weapon, weaponTarget);
            currentWeapon.Init(weaponData.WeaponStats, stats.damage, CharacterType.Enemy);
            currentWeapon.GetComponent<SpriteRenderer>().sortingLayerName = "EnemyWeapon";
            Weapon = currentWeapon;
            _progressbarController = progressbarController;
            EnemyStats = stats;
            _hp = EnemyStats.hp;
            _enemyType = enemyType;
            OnDieAction = onDieAction;
            _signalBus.Subscribe<PlayerDiedSignal>(DisableObject);
        }

        public virtual void TakeDamage(int damage)
        {
            _hp -= damage;
            PlayHitTint();
            _progressbarController.SetDamage(_hp / (float)EnemyStats.hp);

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
            OnDieAction?.Invoke();

            if (_progressbarController != null)
                Destroy(_progressbarController.gameObject);

            _signalBus.Unsubscribe<PlayerDiedSignal>(DisableObject);
        }

        private void PlayHitTint()
        {
            head.color = _hitTintColor;
            Invoke(nameof(SetWhiteColor), 0.2f);
        }

        private void SetWhiteColor() => head.color = Color.white;
    }
}