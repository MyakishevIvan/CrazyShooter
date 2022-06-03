using System;
using System.Collections;
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
        [SerializeField] private float _openingSpeed;
        private BorderType _borderType;

        public GameObject Door => door;
        public DirectionType Direction => directionType;
        private void Awake()
        {
            _borderType = BorderType.Wall;
            door.SetActive(false);
            wall.SetActive(true);
        }

        public void SetState(BorderType borderType)
        {
            _borderType = borderType;
            
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
        
        public void OpenDoor()
        {
            if(_borderType != BorderType.Door)
                return;
            
            _borderType = BorderType.Opening;

            var openingDistance = Door.transform.position;
            var doorLength =  Door.GetComponent<SpriteRenderer>().size.x;

            if (directionType == DirectionType.Left || directionType == DirectionType.Right)
                openingDistance = new Vector3(openingDistance.x, openingDistance.y + doorLength, openingDistance.z);
            else if (directionType == DirectionType.Bottom || directionType == DirectionType.Top)
                openingDistance = new Vector3(openingDistance.x + doorLength, openingDistance.y, openingDistance.z);

            StartCoroutine(StartOpeningDoor(openingDistance));
        }

        private void OnEnable()
        {
            StopAllCoroutines();
        }

        private IEnumerator StartOpeningDoor(Vector3 direction)
        {
            while (direction != Door.transform.position)
            {
                var newPos = Vector3.Lerp(Door.transform.position, direction, Time.deltaTime * _openingSpeed);
                Door.transform.position = newPos;
                yield return null;
            }
            
            _borderType = BorderType.Open;
            StopAllCoroutines();
        }
    }
}

