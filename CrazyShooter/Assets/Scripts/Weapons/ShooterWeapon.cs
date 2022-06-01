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
        private GunStats _gunStats;
        private Vector3 _shootingVector;
        public bool IsShooting { get; set; }

        private GunStats GunStats => (GunStats)_balance.WeaponsConfig.WeaponsDataDict[Enum.WeaponType.Gun].WeaponStats;
      



        public override void Init(WeaponStats weaponStats)
        {
            _gunStats = (GunStats)weaponStats;
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
                    currentBullet.Init(_gunStats.Bulletspeed, _gunStats.Damage, _shootingVector);
                    yield return new WaitForSeconds(_gunStats.ReloadTime);
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