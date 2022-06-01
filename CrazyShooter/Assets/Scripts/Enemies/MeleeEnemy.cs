using System;
using CrazyShooter.Configs;
using CrazyShooter.Core;
using CrazyShooter.Weapons;
using UnityEngine;
using Zenject;

namespace CrazyShooter.Enemies
{
    public class MeleeEnemy : Enemy
    {
        private PlayerView _player;
        private Vector3 _initialPos;
        private bool _canHit;
        private bool _isRightEnemySide = true;
        private MeleeWeapon _meleeWeapon => (MeleeWeapon)Weapon;

        private void Start()
        {
            _initialPos = transform.position;
        }

        private void Update()
        {
            if (!IsAttacking)
            {
                if (_initialPos != transform.position)
                    transform.position = Vector2.MoveTowards(transform.position,
                        _initialPos, EnemyStats.runSpeed * Time.deltaTime);

                return;
            }

            transform.position = Vector2.MoveTowards(transform.position,
                _player.transform.position, EnemyStats.runSpeed * Time.deltaTime);

            if (transform.position.x > _player.transform.position.x && !_isRightEnemySide)
            {
                _isRightEnemySide = true;
                Flip();
            }
            else if (transform.position.x < _player.transform.position.x && _isRightEnemySide)
            {
                _isRightEnemySide = false;
                Flip();
            }

            if (_canHit && _meleeWeapon.IsAttacking == false)
                _meleeWeapon.StartAttack();
        }

        private void Flip()
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x,
                transform.localScale.y, transform.localScale.z);
        }

        public override void Attack(PlayerView player)
        {
            animator.SetBool("isRun", true);
            _player = player;
            IsAttacking = true;
        }

        public override void StopAttack()
        {
            animator.SetBool("isRun", false);
            IsAttacking = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Interact"))
                _canHit = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Interact"))
                _canHit = false;
        }
    }
}