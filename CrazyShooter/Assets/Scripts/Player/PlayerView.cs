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
        [SerializeField] private PlayerType playerType;
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Transform weaponTarget;
        [Inject] DiContainer _diContainer;
        public PlayerController _playerController;

        public Rigidbody2D Rigidbody2D => rigidbody2D;
        public PlayerType PlayerType => playerType;

        public void Init(WeaponData weaponData, PlayerController controller, InteractionsController interactionsController, PlayerStats playerStats)
        {
            _playerController =  _diContainer.InstantiatePrefabForComponent<PlayerController>(controller, transform);
            _diContainer.InstantiatePrefabForComponent<InteractionsController>(interactionsController, transform);
            var weapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weaponData.Weapon, weaponTarget);
            weapon.Init(weaponData.WeaponStats, playerStats.Damage, CharacterType.PLayer);
            weapon.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            _playerController.SetWeapon(weapon, playerStats);
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