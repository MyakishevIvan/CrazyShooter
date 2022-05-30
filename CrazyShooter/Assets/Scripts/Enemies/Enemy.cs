using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enums;
using CrazyShooter.Weapons;
using UnityEngine;
using Zenject;

namespace  CrazyShooter.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform weaponTarget;
        [Inject] DiContainer _diContainer;


        public virtual void InitWeapon(Weapon weapon)
        {
            var currentWeapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weapon.gameObject, weaponTarget);
            currentWeapon.Init();
            currentWeapon.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
  
        }

    }
    
}
