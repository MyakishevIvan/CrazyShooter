using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrayzShooter.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Joystick moveJoystick;
        [SerializeField] private Joystick shootJoystick;
        private PlayerView _playerView;
        private Vector2 _moveVector;
        private bool _isRun;
        private bool _isRightPlayerSide = true;

        public float Speed { get; set; }

        private void Awake()
        {
            _playerView = GetComponentInParent<PlayerView>();
        }
        private void Update()
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

        void FixedUpdate()
        {
            _playerView.Rigidbody2D.MovePosition(_playerView.Rigidbody2D.position + _moveVector * Speed * Time.fixedDeltaTime);
        }
    }
}
