using System.Collections;
using CrazyShooter.Configs;
using CrazyShooter.Enums;
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
        private int _totalDamage;
        private CharacterType _weaponOwner;
        public bool IsShooting { get; set; }

        private GunStats GunStats => (GunStats)_balance.WeaponsConfig.WeaponsDataDict[Enum.WeaponType.Gun].WeaponStats;
      



        public override void Init(WeaponStats weaponStats, int characterDamage, CharacterType weaponOwner)
        {
            _gunStats = (GunStats)weaponStats;
            _totalDamage = (int)_gunStats.Damage + characterDamage;
            _weaponOwner = weaponOwner;
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
                    currentBullet.Init(_gunStats.Bulletspeed, _totalDamage, _weaponOwner);
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