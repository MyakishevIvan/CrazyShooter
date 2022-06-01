using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enums;
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

        public PlayerController _playerController { get; set; }
        public Rigidbody2D Rigidbody2D => rigidbody2D;

        public void SetWeapon(ref Weapon weapon)
        {
             weapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weapon.gameObject, weaponTarget);
             weapon.Init();
             weapon.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        }
        public void PlayAnimation(bool isRun)
        {
            animator.SetBool("isRun", isRun);
        }

        public void Flip()
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

    }
}