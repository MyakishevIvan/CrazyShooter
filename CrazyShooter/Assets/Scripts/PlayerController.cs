using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrayzShooter.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Joystick moveJoystick;
        [SerializeField] private Joystick shootJoystick;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveVector;

        public float Speed { get; set; }

        private void Awake()
        {
            _rigidbody2D = GetComponentInParent<Rigidbody2D>();
        }
        private void Update()
        {
            _moveVector = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);

            if (_moveVector == Vector2.zero)
                _moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

  
        }

        void FixedUpdate()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + _moveVector.normalized * Speed * Time.fixedDeltaTime);
        }
    }
}
