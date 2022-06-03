using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Configs;
using CrazyShooter.Enums;
using CrazyShooter.Interactions;
using CrazyShooter.Progressbar;
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
        [SerializeField] private Transform progressbarPos;
        [SerializeField] private PlayerType playerType;
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Transform weaponTarget;
        [SerializeField] private SpriteRenderer head;
        [Inject] DiContainer _diContainer;
        public PlayerController PlayerController { get; private set; }

        public SpriteRenderer Head => head;
        public Rigidbody2D Rigidbody2D => rigidbody2D;
        public PlayerType PlayerType => playerType;
        public Animator Animator => animator;
        public Transform ProgressbarPos => progressbarPos;

        public void Init(HpProgressbarController hpBarController ,WeaponData weaponData, PlayerController controller,
            InteractionsController interactionsController, PlayerStats playerStats, Action onDieAction)
        {
            PlayerController =  _diContainer.InstantiatePrefabForComponent<PlayerController>(controller, transform);
            _diContainer.InstantiatePrefabForComponent<InteractionsController>(interactionsController, transform);
            var weapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weaponData.Weapon, weaponTarget);
            weapon.Init(weaponData.WeaponStats, playerStats.Damage, CharacterType.PLayer);
            weapon.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            PlayerController.Init(hpBarController, weapon, playerStats, onDieAction);
        }
    }
}