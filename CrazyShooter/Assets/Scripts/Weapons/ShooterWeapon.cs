using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using CrazyShooter.Configs;
using UnityEngine;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace CrazyShooter.Weapons
{
    public class ShooterWeapon : Weapon
    {
        [SerializeField] private Transform bulletStartPos;
        [SerializeField] private Bullet bullet;
        [Inject] private BalanceStorage _balance;
        [Inject] private DiContainer _diContainer;
        private Vector3 _shootingVector;
        private bool _canShoot;
        [SerializeField] private Joystick shootJoystick;

        private GunParams GunParams => _balance.WeaponsConfig.GunParams;
        private float offset => transform.lossyScale.x >= 0 ? 0 : 180;
        private float BulletSpeed { get; set; }
        private float ReloadTime { get; set; }

        private void Start()
        {
        }

        public override void Init(Joystick joystick)
        {
            shootJoystick = joystick;
            BulletSpeed = GunParams.Bulletspeed;
            ReloadTime = GunParams.ReloadTime;
            StartCoroutine(Shooting());
        }

        private void Update()
        {
            _shootingVector = new Vector3(shootJoystick.Horizontal, shootJoystick.Vertical);
            
            if (_shootingVector == Vector3.zero)
                _shootingVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            var angel = Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angel + offset);
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0))
#else
                if(Input.touchCount> 0)
#endif
            
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
                    var currentBullet =
                        _diContainer.InstantiatePrefabForComponent<Bullet>(bullet, newPos, bulletStartPos.rotation,
                            transform);
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