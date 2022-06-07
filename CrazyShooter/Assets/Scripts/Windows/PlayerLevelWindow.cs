using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Core;
using CrazyShooter.UI;
using PolyAndCode.UI;
using SMC.Windows;
using UnityEngine;

namespace CrazyShooter.Windows
{
    public class PlayerLevelWindow : Window<PlayerLevelWindowSetup>
    {
        [SerializeField] private RecyclableScrollRect weaponScroll;
        [SerializeField] private RecyclableScrollRect levelScroll;
        private PlayerLevelWindowSetup _setup;
        public override void Setup(PlayerLevelWindowSetup setup)
        {
            _setup = setup;
            weaponScroll.Initialize(setup.weaponScrollDataSource);
            levelScroll.Initialize(setup.levelScrollDataSource);
            setup.weaponScrollDataSource.OnItemSelected += SetAllWeaponCellUnselected;
        }

        private void SetAllWeaponCellUnselected()
        {
            weaponScroll.ForEachCell(SetItemAsDisabled);
        }
        
        private void SetItemAsDisabled(ICell cell)
        {
            (cell as WeaponCell)?.SetAsSelected(false);
        }

        public void OnDestroy()
        {
            _setup.weaponScrollDataSource.OnItemSelected -= SetAllWeaponCellUnselected;
        }
    }

    public class PlayerLevelWindowSetup : WindowSetup
    {
        public ItemsScrollDataSource<WeaponCellSetup> weaponScrollDataSource;
        public ItemsScrollDataSource<LevelCellSetup> levelScrollDataSource;
    }
}
