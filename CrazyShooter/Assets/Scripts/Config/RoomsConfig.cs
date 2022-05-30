using System;
using System.Collections.Generic;
using CrazyShooter.Enums;
using CrazyShooter.Rooms;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "RoomsConfig", menuName = "Configs/RoomsConfig")]
    public class RoomsConfig : ScriptableObject
    {
        [SerializeField] private List<BaseRoom> rooms;
        [SerializeField] private RoomData roomsData;
        private Dictionary<RoomType, BaseRoom> _roomsDict;

        public Dictionary<RoomType, BaseRoom> RoomsDict => _roomsDict?? InitRoomsDictionaryDict();
        public RoomData RoomsData => roomsData;

        private Dictionary<RoomType, BaseRoom>  InitRoomsDictionaryDict()
        {
            _roomsDict = new Dictionary<RoomType, BaseRoom>();
            
            foreach (var room in rooms)
                _roomsDict.Add(room.RoomType, room);
            
            return _roomsDict;
        }
    }

    [Serializable]
    public class RoomData
    {
        public RoomType roomType;
        public RoomDirectionType roomDirectionType;
        public List<RoomData> dependentRooms;
    }
    
}