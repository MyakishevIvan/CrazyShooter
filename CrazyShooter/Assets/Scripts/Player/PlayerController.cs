using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Configs;
using CrazyShooter.Enum;
using CrazyShooter.Signals;
using CrazyShooter.System;
using CrazyShooter.Weapons;
using CrazyShooter.Windows;
using SMC.Windows;
using UnityEngine;
using Zenject;

namespace CrazyShooter.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Joystick moveJoystick;
        [SerializeField] private Joystick shootJoystick;
        [Inject] private BalanceStorage _balance;
        [Inject] private SceneTransitionSystem _sceneTransitionSystem;
        [Inject] private WindowsManager _windowsManager;
        [Inject] private SignalBus _signalBus;
        private Action OnDieAction; 
        private PlayerView _playerView;
        private Vector2 _moveVector;
        private bool _isRun;
        private bool _isRightPlayerSide = true;
        private ShooterWeapon _shootingWeapon;
        private MeleeWeapon _meleeWeapon;
        private bool _isShootingWeapon;
        private PlayerStats _playerStats;
        private int _hp;

        private float ShootOffset => _shootingWeapon.transform.lossyScale.x >= 0 ? 0 : 180;

        private void Awake()
        {
            _playerView = GetComponentInParent<PlayerView>();
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
                                                 _moveVector * (_playerStats.PlayerSpeed * Time.fixedDeltaTime));
        }

        private void PlayerAttackUpdate(bool canAttack)
        {
            if (_shootingWeapon != null)
            {
                var _shootingVector = new Vector3(shootJoystick.Horizontal, shootJoystick.Vertical);

                if (_shootingVector == Vector3.zero)
                    _shootingVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) -
                                      _shootingWeapon.transform.position;

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
                _playerView.Animator.SetBool("isRun", _isRun);
            }
            else if (_moveVector != Vector2.zero && !_isRun)
            {
                _isRun = true;
                _playerView.Animator.SetBool("isRun", _isRun);
            }

            if (_isRightPlayerSide && _moveVector.x < 0)
            {
                _isRightPlayerSide = false;
                Flip();
            }
            else if (!_isRightPlayerSide && _moveVector.x > 0)
            {
                _isRightPlayerSide = true;
                Flip();
            }
        }

        private void Flip()
        {
            var localScale = _playerView.transform.localScale;
            localScale =
                new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            _playerView.transform.localScale = localScale;
        }

        public void Init(Weapon weapon, PlayerStats playerStats, Action onDieAction)
        {
            _playerStats = playerStats;
            OnDieAction = onDieAction;
            _hp = _playerStats.Hp;
            if (weapon is ShooterWeapon shooterWeapon)
                _shootingWeapon = shooterWeapon;
            else
                _meleeWeapon = weapon as MeleeWeapon;

            shootJoystick.gameObject.SetActive(_shootingWeapon != null);
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            Debug.LogError("Player HP " + _hp);
            if (_hp <= 0)
            {
                _playerView.Animator.SetTrigger("death");
                _signalBus.Fire(new PlayerDiedSignal());
                moveJoystick.gameObject.SetActive(false);
                shootJoystick.gameObject.SetActive(false);
                Invoke("DieAction", 2);
                this.enabled = false;
            }
        }
        
        private void DieAction()=> OnDieAction?.Invoke();
        
    }
}