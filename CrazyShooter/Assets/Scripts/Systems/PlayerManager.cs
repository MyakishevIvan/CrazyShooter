using System.Collections.Generic;
using CrazyShooter.Configs;
using CrazyShooter.Enum;
using CrazyShooter.Profiles;
using SMC.Profile;
using UnityEngine;
using Zenject;

public class PlayerManager : ProfileManager<PlayerProfile>
{
    [Inject] private BalanceStorage _balance;
    private MapsConfig _mapsConfig => _balance.MapsConfig;
    public PlayerManager() : base("PlayerProfile")
    {
    }

    protected override void OnProfileSet()
    {
    }

    protected override PlayerProfile CreateDefaultProfile()
    {
        var porfile = new PlayerProfile()
        {
            currentWeapon = WeaponType.Gun,
            currentMapLevel = 0,
            availableWeapons = new List<WeaponType>()
            {
                WeaponType.Gun,
                WeaponType.Sword,
                WeaponType.SmallGun
            }
        };

        return porfile;
    }

    public void IncreaseMapLevel()
    {
        if(_mapsConfig.MapsCount - 1 == Profile.currentMapLevel)
            return;
        
        Profile.currentMapLevel++;
    }

    public void AddNewWeapon(WeaponType weaponType)
    {
        var isNewWeapon = !Profile.availableWeapons.Contains(weaponType);

        if (!isNewWeapon)
        {
            Debug.LogError("weapon already added");
            return;
        }

        Profile.availableWeapons.Add((weaponType));
    }

    public List<WeaponType> GetAvailableWeapons()
    {
        return Profile.availableWeapons;
    }

    public int GetCurrentMapLevel()
    {
        return Profile.currentMapLevel;
    }

    public WeaponType GetCurrentWeapon()
    {
        return Profile.currentWeapon;
    }

    public void ChangeCurrentWeapon(WeaponType weaponType)
    {
        if (Profile.availableWeapons.Contains(weaponType))
            Profile.currentWeapon = weaponType;
        else
            Debug.LogError("Weapon must be available");
    }
}