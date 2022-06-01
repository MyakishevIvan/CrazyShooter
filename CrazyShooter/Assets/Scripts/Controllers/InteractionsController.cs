using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Rooms;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CrazyShooter.Interactions
{
    public class InteractionsController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.layer != LayerMask.NameToLayer("Border"))
                return;
            
            var border = collider.gameObject.GetComponentInParent<Border>();
            border.OpenDoor();
        }
    }
}
