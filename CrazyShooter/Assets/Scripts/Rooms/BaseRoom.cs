using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enums;
using UnityEditor;
using UnityEngine;

namespace CrazyShooter.Rooms
{
    public  class BaseRoom : MonoBehaviour
    {
        [SerializeField] private RoomType roomType;
        [SerializeField] private GameObject plane;
        [SerializeField] private GameObject leftBorder;
        [SerializeField] private GameObject rightBorder;
        [SerializeField] private GameObject topBorder;
        [SerializeField] private GameObject bottomBorder;
        [SerializeField] private int index;

        public GameObject Plane => plane;
        public GameObject LeftBorder => leftBorder;
        public GameObject RightBorder => rightBorder;
        public GameObject TopBorder => topBorder;
        public GameObject BottomBorder => bottomBorder;
        public RoomType RoomType => roomType;
       
        
#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            var roomScript = GetComponent<BaseRoom>();

            if (roomScript == this)
                return;

            plane = roomScript.plane;
            leftBorder = roomScript.leftBorder;
            rightBorder = roomScript.rightBorder;
            topBorder = roomScript.topBorder;
            bottomBorder = roomScript.bottomBorder;
            
            void DestroyBaseScript()
            {
                EditorApplication.delayCall -= DestroyBaseScript;
                DestroyImmediate(roomScript, true);
            }

            EditorApplication.delayCall += DestroyBaseScript;
        }
#endif

        private void Awake()
        {
            leftBorder.SetActive((true));
            rightBorder.SetActive((true));
            TopBorder.SetActive((true));
            BottomBorder.SetActive((true));
        }

        public Vector3 GetRoomPosition(BaseRoom installedRoom, RoomDirectionType directionType)
        {
            var sideCenter = Vector3.zero;
            var posOffset = 0f;
            switch (directionType)
            {
                case RoomDirectionType.Left:
                     posOffset = transform.position.x - GetRoomSize().x / 2
                                                         - installedRoom.GetRoomSize().x / 2;
                    sideCenter = new Vector3( posOffset, leftBorder.transform.position.y, 0);
                     
                    leftBorder.gameObject.SetActive(false);
                    installedRoom.RightBorder.gameObject.SetActive(false);
                    break;
                case RoomDirectionType.Right:
                     posOffset = transform.position.x + GetRoomSize().x / 2
                                                         + installedRoom.GetRoomSize().x / 2;
                     
                    sideCenter = new Vector3( posOffset, rightBorder.transform.position.y, 0);
                    rightBorder.gameObject.SetActive(false);
                    installedRoom.LeftBorder.gameObject.SetActive(false);
                    break;
                case RoomDirectionType.Top:
                    posOffset = transform.position.y + GetRoomSize().y / 2
                                                     + installedRoom.GetRoomSize().y / 2;
                    
                    sideCenter = new Vector3( topBorder.transform.position.x, posOffset, 0);
                    topBorder.gameObject.SetActive(false);
                    installedRoom.BottomBorder.gameObject.SetActive(false);
                    break;
                case RoomDirectionType.Bottom:
                    posOffset = transform.position.y - GetRoomSize().y / 2
                                                     - installedRoom.GetRoomSize().y / 2;
                    
                    sideCenter = new Vector3( topBorder.transform.position.x, posOffset, 0);
                    bottomBorder.gameObject.SetActive(false);
                    installedRoom.TopBorder.gameObject.SetActive(false);
                    break;
                default:
                    Debug.LogError("There is no case for directionType " + directionType);
                    break;
            }
            return sideCenter;
        }

        public Vector2 GetRoomSize()
        {
            var sprite = Plane.GetComponent<SpriteRenderer>();
            return sprite.size;
        }
    }
    
}
