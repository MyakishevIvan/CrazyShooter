using CrazyShooter.Core;
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

        public virtual void InitWeapon(CharacterType characterType, Weapon weapon)
        {
            var currentWeapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weapon.gameObject, weaponTarget);
            currentWeapon.Init();
            var sortingLayerName = characterType == CharacterType.PLayer ? "PlayerWeapon" : "EnemyWeapon";
            currentWeapon.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
            Weapon = currentWeapon;
        }

        public abstract void Attack(PlayerView player);
        public abstract void StopAttack();
    }
    
}
