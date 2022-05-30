﻿using System;
using System.Collections.Generic;
using CrazyShooter.Enums;
using CrazyShooter.Rooms;
using Enums;
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
        [SerializeField]
        private List<BorderState> borderStates;
        public RoomType roomType;
        public DirectionType directionType;
        public List<RoomData> dependentRooms;

        public List<BorderState> BorderStates => borderStates;
    }

    [Serializable]
    public class BorderState
    {
        public DirectionType borderSide;
        public BorderType borderType;
    }
    
}