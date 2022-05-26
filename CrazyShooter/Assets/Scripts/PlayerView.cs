using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace CrayzShooter.Core
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rigidbody2D;
        private bool _previouseSide;


        public Rigidbody2D Rigidbody2D => rigidbody2D;

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