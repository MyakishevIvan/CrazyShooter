using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enum;
using SMC.Profile;
using UnityEngine;

namespace CrazyShooter.Profiles
{
   public class PlayerProfile : IProfile
   {
      public List<WeaponType> availableWeapons;
      public int currentMapLevel;
      public WeaponType currentWeapon;
   }
}
