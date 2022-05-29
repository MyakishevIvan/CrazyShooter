using System.Collections.Generic;
using CrazyShooter.Rooms;
using UnityEngine;

namespace CrazyShooter.Configs
{
    [CreateAssetMenu(fileName = "RoomsConfig", menuName = "Configs/RoomsConfig")]
    public class RoomsConfig : ScriptableObject
    {
        [SerializeField] private List<BaseRoom> rooms;

        public List<BaseRoom> Rooms => rooms;
    }
}