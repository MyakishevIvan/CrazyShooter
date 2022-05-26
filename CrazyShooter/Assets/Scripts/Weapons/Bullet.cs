using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrayzShooter.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        private float Speed { get; set; }
        private float Damage { get; set; }
        private Vector3 Target { get; set; }

        public void Init(float speed, float damage, Vector3 target)
        {
            Speed = speed;
            Damage = damage;
            Target = target;
            Invoke("DestoyBullet", 5);
        }

        private void Update()
        {

            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
            }
        }

        private void DestoyBullet()
        {
            Destroy(gameObject);
        }
    }
}
