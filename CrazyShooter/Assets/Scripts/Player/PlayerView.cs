using CrayzShooter.Weapons;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Signals;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;

namespace CrayzShooter.Core
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Transform weaponTarget;
        [SerializeField] private Weapon weapon;
        [SerializeField] private GameObject Head;
        [Inject] DiContainer _diContainer;
        [Inject] private SignalBus _signal;

        public Rigidbody2D Rigidbody2D => rigidbody2D;

        public void InitWeapon(Weapon weapon)
        {
            var currentWeapon = _diContainer.InstantiatePrefabForComponent<Weapon>(weapon.gameObject, weaponTarget);
            currentWeapon.Init();
            currentWeapon.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
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