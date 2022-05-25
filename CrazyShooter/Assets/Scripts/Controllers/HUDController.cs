using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _shootJoystick;

    public Joystick MoveJoystick => _moveJoystick;
    public Joystick ShootJoystick => _shootJoystick;
}
