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
        [SerializeField] private Border leftBorder;
        [SerializeField] private Border rightBorder;
        [SerializeField] private Border topBorder;
        [SerializeField] private Border bottomBorder;
        [SerializeField] private int index;

        public GameObject Plane => plane;
        public Border LeftBorder => leftBorder;
        public Border RightBorder => rightBorder;
        public Border TopBorder => topBorder;
        public Border BottomBorder => bottomBorder;
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
            leftBorder.gameObject.SetActive((true));
            rightBorder.gameObject.SetActive((true));
            TopBorder.gameObject.SetActive((true));
            BottomBorder.gameObject.SetActive((true));
        }

        public Vector3 GetRoomPosition(BaseRoom installedRoom, DirectionType directionType)
        {
            var sideCenter = Vector3.zero;
            var posOffset = 0f;
            switch (directionType)
            {
                case DirectionType.Left:
                     posOffset = transform.position.x - GetRoomSize().x / 2
                                                         - installedRoom.GetRoomSize().x / 2;
                    sideCenter = new Vector3( posOffset, leftBorder.transform.position.y, 0);
                     
                    installedRoom.RightBorder.gameObject.SetActive(false);
                    break;
                case DirectionType.Right:
                     posOffset = transform.position.x + GetRoomSize().x / 2
                                                         + installedRoom.GetRoomSize().x / 2;
                     
                    sideCenter = new Vector3( posOffset, rightBorder.transform.position.y, 0);
                    installedRoom.LeftBorder.gameObject.SetActive(false);
                    break;
                case DirectionType.Top:
                    posOffset = transform.position.y + GetRoomSize().y / 2
                                                     + installedRoom.GetRoomSize().y / 2;
                    
                    sideCenter = new Vector3( topBorder.transform.position.x, posOffset, 0);
                    installedRoom.BottomBorder.gameObject.SetActive(false);
                    break;
                case DirectionType.Bottom:
                    posOffset = transform.position.y - GetRoomSize().y / 2
                                                     - installedRoom.GetRoomSize().y / 2;
                    
                    sideCenter = new Vector3( topBorder.transform.position.x, posOffset, 0);
                    installedRoom.TopBorder.gameObject.SetActive(false);
                    break;
                default:
                    Debug.LogError("There is no case for directionType " + directionType);
                    break;
            }
            return sideCenter;
        }

        private Vector2 GetRoomSize()
        {
            var sprite = Plane.GetComponent<SpriteRenderer>();
            return sprite.size;
        }

        public void OpenDoor()
        {
            
        }
    }
    
}
