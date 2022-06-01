using CrazyShooter.Configs;
using CrazyShooter.Enums;
using UnityEngine;
using Zenject;

namespace CrazyShooter.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [Inject] private BalanceStorage _balance;
        private float _speed;
        private float _damage;
        private CharacterType _weaponOwner;

        private PlayerConfig _playerConfig => _balance.PlayerConfig;

        public void Init(float speed, float damage, CharacterType weaponOwner)
        {
            _speed = speed;
            _damage = damage;
            _weaponOwner = weaponOwner;
            Invoke("DestoyBullet", 5);
        }

        private void Update()
        {

            transform.Translate(Vector2.right * (_speed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_weaponOwner == CharacterType.Enemy)
            {
                if (collision.gameObject.layer == _playerConfig.PlayerLayer)
                {
                    Debug.LogError("Hit Player Damage " + _damage);
                    Destroy(gameObject);
                }
            }
            else if (_weaponOwner == CharacterType.PLayer)
            {
                if (collision.gameObject.layer == _playerConfig.EnemyLayer)
                {
                    Debug.LogError("Hit Enemy");
                    Destroy(gameObject);
                }
                
            }
        }

        private void DestoyBullet()
        {
            Destroy(gameObject);
        }
    }
}
