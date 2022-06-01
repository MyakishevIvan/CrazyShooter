using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Configs;
using CrazyShooter.Enums;
using CrazyShooter.Interactions;
using CrazyShooter.Signals;
using CrazyShooter.Weapons;
using Enums;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;

namespace CrazyShooter.Core
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Transform weaponTarget;
        [Inject] DiContainer _diContainer;

        public Rigidbody2D Rigidbody2D => rigidbody2D;

        public void Init(WeaponData weaponData, PlayerConfig playerConfig)
        {
           var playerController =  _diContainer.InstantiatePrefabForComponent<PlayerController>(playerConfig.PlayerController, transform);
            _diContainer.InstantiatePrefabForComponent<InteractionsController>(playerConfig.InteractionsController, transform);
            var weapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weaponData.Weapon, weaponTarget);
            weapon.Init(weaponData.WeaponStats);
            weapon.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            playerController.SetWeapon(weapon);
        }

        public void PlayAnimation(bool isRun)
        {
            animator.SetBool("isRun", isRun);
        }

        public void Flip()
        {
            transform.localScale =
                new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}