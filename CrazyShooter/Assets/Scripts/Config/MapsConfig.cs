using System;
using System.Collections.Generic;
using System.Linq;
using CrazyShooter.Enums;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "MapsConfig", menuName = "Configs/MapsConfig")]
    public class MapsConfig : ScriptableObject
    {
        public int MapsCount => mapsData.Count;

        [SerializeField] private List<MapData> mapsData;

        public MapData GetRoomsConfig(int id)
        {
            var result = mapsData.FirstOrDefault(x => x.mapId == id);

            if (result == null)
                throw new Exception("There is no MapData for id " + id);

            return result;
        }
        
    }
    
    [Serializable]
    public class MapData
    {
        public int mapId;
        public RoomsConfig roomsConfig;
        public DifficultyType enemyDifficalt;
        public DifficultyType bossDifficalt;
    } 
}