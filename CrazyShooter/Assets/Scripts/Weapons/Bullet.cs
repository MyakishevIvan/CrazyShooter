using CrazyShooter.Configs;
using CrazyShooter.Core;
using CrazyShooter.Enemies;
using CrazyShooter.Enums;
using UnityEngine;
using Zenject;

namespace CrazyShooter.Weapons
{
    public class Bullet : MonoBehaviour
    {
        private float _speed;
        private int _damage;
        private CharacterType _weaponOwner;
        
        public void Init(float speed, int damage, CharacterType weaponOwner)
        {
            _speed = speed;
            _damage = damage;
            _weaponOwner = weaponOwner;
            Destroy(gameObject, 5);
        }

        private void Update()
        {

            transform.Translate(Vector2.right * (_speed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_weaponOwner == CharacterType.Enemy)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    var player = collision.gameObject.GetComponent<PlayerView>().PlayerController;
                    player.TakeDamage(_damage);
                    Destroy(gameObject);
                }
            }
            else if (_weaponOwner == CharacterType.PLayer)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    var enemy = collision.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(_damage);
                    Destroy(gameObject);
                }
                
            }
            
            if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
                Destroy(gameObject);
        }

    }
}
