using System;
using CrazyShooter.Enums;
using Enums;
using UnityEngine;

namespace CrazyShooter.Rooms
{
    public class Border : MonoBehaviour
    {
        [SerializeField] private GameObject wall;
        [SerializeField] private GameObject door;
        [SerializeField] private DirectionType directionType;

        public GameObject Door => door;
        public DirectionType Direction => directionType;
        private void Awake()
        {
            door.SetActive(false);
            wall.SetActive(true);
        }

        public void SetState(BorderType borderType)
        {
            switch (borderType)
            {
                case BorderType.Door:
                    door.SetActive(true);
                    wall.SetActive(false);
                    break;
                case BorderType.Open:
                    door.SetActive(false);
                    wall.SetActive(false);
                    break;
                default:
                    if (borderType == BorderType.Wall)
                        Debug.LogError("Border is default Wall");
                    else
                        Debug.LogError("There is no case for type " + borderType);

                    break;
            }
        }
    }
}

