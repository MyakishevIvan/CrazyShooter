using CrayzShooter.Weapons;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public Rigidbody2D Rigidbody2D => rigidbody2D;

        public void InitWeapon(Weapon weapon)
        {
            var gun = _diContainer.InstantiatePrefabForComponent<Weapon>(weapon.gameObject, weaponTarget);
            gun.Init();
            gun.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        }
        public void PlayAnimation(bool isRun)
        {
            animator.SetBool("isRun", isRun);
        }

        public void Flip()
        {
            Head.transform.localScale = new Vector3(-1 * Head.transform.localScale.x, Head.transform.localScale.y, Head.transform.localScale.z);
        }
    }
}