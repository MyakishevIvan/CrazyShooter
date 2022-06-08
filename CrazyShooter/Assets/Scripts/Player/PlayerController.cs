using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Configs;
using CrazyShooter.Enum;
using CrazyShooter.Progressbar;
using CrazyShooter.Signals;
using CrazyShooter.System;
using CrazyShooter.Weapons;
using CrazyShooter.Windows;
using SMC.Windows;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CrazyShooter.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Joystick moveJoystick;
        [SerializeField] private Joystick shootJoystick;
        [Inject] private SignalBus _signalBus;
        private Action OnDieAction; 
        private PlayerView _playerView;
        private HpProgressbarController _hpBarController;
        private Color _hitTintColor = Color.red;
        private Vector2 _moveVector;
        private bool _isRun;
        private bool _isRightPlayerSide = true;
        private ShooterWeapon _shootingWeapon;
        private MeleeWeapon _meleeWeapon;
        private bool _isShootingWeapon;
        private PlayerStats _playerStats;
        private int _hp;
        private int _uiLayer = 1 << 5;

        private float ShootOffset => _shootingWeapon.transform.lossyScale.x >= 0 ? 0 : 180;

        private void Awake()
        {
#if  UNITY_EDITOR || UNITY_STANDALONE
            DisableJoystick();
#endif
            _playerView = GetComponentInParent<PlayerView>();
            _signalBus.Subscribe<PlayerWinSignal>(DisableJoystick);
        }

        private void Update()
        {
            PlayerMoveUpdate();

#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0))

#else
                if(Input.touchCount > 0)
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
            if(EventSystem.current.IsPointerOverGameObject())
                return;

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

        public void Init(HpProgressbarController hpBarController, Weapon weapon, PlayerStats playerStats, Action onDieAction)
        {
            _playerStats = playerStats;
            OnDieAction = onDieAction;
            _hp = _playerStats.Hp;
            _hpBarController = hpBarController;
            
            if (weapon is ShooterWeapon shooterWeapon)
                _shootingWeapon = shooterWeapon;
            else
                _meleeWeapon = weapon as MeleeWeapon;

            shootJoystick.gameObject.SetActive(shootJoystick.gameObject.activeSelf && _shootingWeapon != null);
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            PlayHitTint();
            var damagePersent = _hp / (float)_playerStats.Hp;
            _hpBarController.SetDamage(damagePersent);
            
            if (_hp <= 0)
            {
                _playerView.Animator.SetTrigger("death");
                _signalBus.Fire(new PlayerDiedSignal());
                DisableJoystick();
                Invoke("DieAction", 2);
                this.enabled = false;
            }
        }

        private void DisableJoystick()
        {
            moveJoystick.gameObject.SetActive(false);
            shootJoystick.gameObject.SetActive(false);
        }

        private void DieAction()=> OnDieAction?.Invoke();
        
        private void PlayHitTint()
        {
            _playerView.Head.color = _hitTintColor;
            Invoke(nameof(SetWhiteColor),0.2f);
        }

        private void SetWhiteColor() =>  _playerView.Head.color = Color.white;

        private void OnDisable()
        {
            _signalBus.Unsubscribe<PlayerWinSignal>(DisableJoystick);
        }
    }
}