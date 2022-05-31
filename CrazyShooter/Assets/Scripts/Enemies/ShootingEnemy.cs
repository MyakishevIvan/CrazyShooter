using CrazyShooter.Core;
using CrazyShooter.Weapons;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace CrazyShooter.Enemies
{
    public class ShootingEnemy : Enemy
    {
        private bool _isAttacking;
        private PlayerView _player;
        private float ShootOffset => Weapon.transform.lossyScale.x >= 0 ? 0 : 180;


        private void Update()
        {
            if (!_isAttacking)
                return;
            
            ((ShooterWeapon)Weapon).IsShooting = true;
            var _shootingVector =  _player.transform.position - Weapon.transform.position;
            var angel = Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg;
            Weapon.transform.rotation = Quaternion.Euler(0f, 0f, angel);
        }

        public override void Attack(PlayerView player)
        {
            _player = player;
            _isAttacking = true;
        }

        public override void StopAttack()
        {
            _isAttacking = false;
            ((ShooterWeapon)Weapon).IsShooting = false;

        }
    }
}