using System.Collections.Generic;
using CrazyShooter.Core;
using CrazyShooter.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrazyShooter.Rooms
{
    public class EnemyRoom : BaseRoom
    {
        public List<Enemy> EnemiesInRoom { get; set; }

        private void Awake()
        {
            Plane.GetComponent<SpriteRenderer>().color =
                Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                var player = col.gameObject.GetComponent<PlayerView>();
                foreach (var enemy in EnemiesInRoom)
                {
                    if(enemy != null)
                    enemy.Attack(player);
                }

            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                foreach (var enemy in EnemiesInRoom)
                {
                    if(enemy != null)
                        enemy.StopAttack();
                }
            }
        }
    }
    
}
