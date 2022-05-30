using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Rooms;
using UnityEngine;

namespace CrazyShooter.Interactions
{
    public class InteractionsController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            var border = collider.gameObject.GetComponentInParent<Border>();
            if(border == null)
                return;
            
            border.OpenDoor();
        }
    }
}
