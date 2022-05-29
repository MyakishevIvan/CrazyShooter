using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Rooms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrazyShooter.Rooms
{
    public class EnemyRoom : BaseRoom
    {
        private void Awake()
        {
            Plane.GetComponent<SpriteRenderer>().color =
                Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }
    
}
