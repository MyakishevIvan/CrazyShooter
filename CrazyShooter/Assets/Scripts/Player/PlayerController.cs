using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Configs;
using CrazyShooter.Enum;
using CrazyShooter.Weapons;
using UnityEngine;
using Zenject;

namespace CrazyShooter.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Joystick moveJoystick;
        [SerializeField] private Joystick shootJoystick;
        [Inject] private BalanceStorage _balance;
        private PlayerView _playerView;
        private Vector2 _moveVector;
        private bool _isRun;
        private bool _isRightPlayerSide = true;
        private ShooterWeapon _shootingWeapon;
        private MeleeWeapon _meleeWeapon;
        private bool _isShootingWeapon;

        private float ShootOffset => _shootingWeapon.transform.lossyScale.x >= 0 ? 0 : 180;
        private PlayerConfig _playerConfig => _balance.PlayerConfig;
        public float Speed { get; set; }
        public Joystick ShootJoystick => shootJoystick;

        private void Awake()
        {
            _playerView = GetComponentInParent<PlayerView>();
            Speed = _playerConfig.PlayerStats.PlayerSpeed;
        }

        private void Update()
        {
            PlayerMoveUpdate();

#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0))
#else
                if(Input.touchCount> 0)
#endif
                PlayerAttackUpdate(true);
            else
            {
                PlayerAttackUpdate(false);
            }
        }

        void FixedUpdate()
        {
            _playerView.Rigidbody2D.MovePosition(_playerView.Rigidbody2D.position +
                                                 _moveVector * (Speed * Time.fixedDeltaTime));
        }

        private void PlayerAttackUpdate(bool canAttack)
        {
            if (_shootingWeapon != null)
            {
                var _shootingVector = new Vector3(shootJoystick.Horizontal, shootJoystick.Vertical);

                if (_shootingVector == Vector3.zero)
                    _shootingVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _shootingWeapon.transform.position;

                var angel = Mathf.Atan2(_shootingVector.y, _shootingVector.x) * Mathf.Rad2Deg;
                _shootingWeapon.transform.rotation = Quaternion.Euler(0f, 0f, angel + ShootOffset);


                _shootingWeapon.IsShooting = canAttack;
            }
            else if (_meleeWeapon != null && _meleeWeapon.IsAttacking == false && canAttack)
            {
                _meleeWeapon.StartAttack();
            }
        }

        private void PlayerMoveUpdate()
        {
            _moveVector = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical).normalized;

            if (_moveVector == Vector2.zero)
                _moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            if (_moveVector == Vector2.zero && _isRun)
            {
                _isRun = false;
                _playerView.PlayAnimation(_isRun);
            }
            else if (_moveVector != Vector2.zero && !_isRun)
            {
                _isRun = true;
                _playerView.PlayAnimation(_isRun);
            }

            if (_isRightPlayerSide && _moveVector.x < 0)
            {
                _isRightPlayerSide = false;
                _playerView.Flip();
            }
            else if (!_isRightPlayerSide && _moveVector.x > 0)
            {
                _isRightPlayerSide = true;
                _playerView.Flip();
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            if (weapon is ShooterWeapon shooterWeapon)
                _shootingWeapon = shooterWeapon;
            else
                _meleeWeapon = weapon as MeleeWeapon;
            
            shootJoystick.gameObject.SetActive(_shootingWeapon != null);
        }
    }
}