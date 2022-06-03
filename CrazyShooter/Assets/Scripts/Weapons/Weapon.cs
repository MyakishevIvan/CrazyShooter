using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Configs;
using CrazyShooter.Enum;
using CrazyShooter.Enums;
using CrazyShooter.Signals;
using Enums;
using UnityEngine;
using Zenject;

namespace CrazyShooter.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;
        public WeaponType WeaponType => weaponType;
        protected WeaponStats _weaponStats;
        [Inject] protected SignalBus _signalBus;

        private void Awake()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(DisableObject);
        }


        public abstract void Init(WeaponStats weaponStats, int characterDamage, CharacterType weaponOwner);

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