using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Core;
using CrazyShooter.UI;
using PolyAndCode.UI;
using SMC.Windows;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace CrazyShooter.Windows
{
    public class PlayerLevelWindow : Window<PlayerLevelWindowSetup>
    {
        [SerializeField] private RecyclableScrollRect weaponScroll;
        [SerializeField] private RecyclableScrollRect levelScroll;
        [SerializeField] private CustomButton playButton;
        [SerializeField] private CustomButton exitButton;
        private PlayerLevelWindowSetup _setup;
        [Inject] private WindowsManager _windowsManager;
        public override void Setup(PlayerLevelWindowSetup setup)
        {
            _setup = setup;
            weaponScroll.Initialize(setup.weaponScrollDataSource);
            levelScroll.Initialize(setup.levelScrollDataSource);
            setup.weaponScrollDataSource.OnItemSelected += SetAllWeaponCellUnselected;
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(() => _setup.OnPlayButton?.Invoke());
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(()=> _windowsManager.Close<PlayerLevelWindow>());
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
        public Action OnPlayButton;
    }
}
