using System.Collections;
using CrazyShooter.Configs;
using Enums;
using UnityEngine;
using Zenject;
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
        public bool IsShooting { get; set; }
        [SerializeField] private Joystick shootJoystick;

        private GunParams GunParams => _balance.WeaponsConfig.GunParams;
        private float BulletSpeed { get; set; }
        private float ReloadTime { get; set; }



        public override void Init(CharacterType characterType, Joystick joystick)
        {
            _characterType = characterType;
            shootJoystick = joystick;
            BulletSpeed = GunParams.Bulletspeed;
            ReloadTime = GunParams.ReloadTime;
            StartCoroutine(Shooting());
        }
        
        private IEnumerator Shooting()
        {
            while (true)
            {
                if (IsShooting)
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