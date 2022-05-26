using CrayzShooter.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CrayzShooter.Weapons
{
    public class ShooterWeapon : Weapon
    {

        [SerializeField] private Transform bulletStartPos;
        [SerializeField] private float offset;
        [SerializeField] private Bullet bullet;
        [Inject] private BalanceStorage _balance;
        private Vector3 _shootingVector;
        private bool _canShoot;

        private GunParams GunParams => _balance.WeaponsConfig.GunParams;
        private float BulletSpeed { get; set; }
        private float ReloadTime { get; set; }

        private void Start()
        {
        }
        public override void Init()
        {
            BulletSpeed = GunParams.Bulletspeed;
            ReloadTime = GunParams.ReloadTime;
            StartCoroutine(Shooting());
        }

        private void Update()
        {
            _shootingVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var angel = Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angel + offset);

            if (Input.GetMouseButton(0))
                _canShoot = true;
            else
                _canShoot = false;
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                if (_canShoot)
                {
                    var newPos = new Vector3(bulletStartPos.position.x, bulletStartPos.position.y, 0);
                    var currentBullet = Instantiate(bullet, newPos, bulletStartPos.rotation);
                    currentBullet.Init(BulletSpeed, Damage, _shootingVector);
                    yield return new WaitForSeconds(ReloadTime);

                }

                yield return null;
            }

        }


        private void OnDisable()
        {
            StopAllCoroutines();
        }
      
    }
}