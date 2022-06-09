using System;
using CrazyShooter.Windows;
using CrazyShooter.Enum;
using CrazyShooter.System;
using SMC.Windows;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using CrazyShooter.Configs;
using CrazyShooter.Enums;
using CrazyShooter.UI;
using UnityEngine;
using Zenject;

public class MenuController : MonoBehaviour
{
    [Inject] private WindowsManager _windowsManager;
    [Inject] private SceneTransitionSystem _sceneTransitionSystem;
    [Inject] private BalanceStorage _balance;
    [Inject] private PlayerManager _playerManager;
    [Inject] private GameModel _gameModel;
    private int _currentMapLevel;
    private MapsConfig MapsConfig => _balance.MapsConfig;

    private void Awake()
    {
        _windowsManager.Open<MenuWindow, MenuWondowSetup>(new MenuWondowSetup
        {
            onPlay = OpenPlayerLevelWindow
        });
    }

    private void OpenPlayerLevelWindow()
    {
        var setup = new PlayerLevelWindowSetup()
        {
            OnPlayButton = OpenLevel,
            weaponScrollDataSource = new ItemsScrollDataSource<WeaponCellSetup>(CreateWeaponCell()),
            levelScrollDataSource = new ItemsScrollDataSource<LevelCellSetup>( CreateLevelCell())
        };

        _windowsManager.Open<PlayerLevelWindow, PlayerLevelWindowSetup>(setup);
    }

    private void OpenLevel()
    {
        _gameModel.InitGameMode(PlayerType.Common, _playerManager.GetCurrentWeapon(),_currentMapLevel);
        _sceneTransitionSystem.GoToScene(SceneType.FighScene, true, true,
            false,  true, OpenPromptWindow);
    }

    private void OpenPromptWindow()
    {
        if(_playerManager.GetCurrentMapLevel() == 0)
            _windowsManager.Open<PromptWindow, PromptWindowSetup>(new PromptWindowSetup()
            {
                pomptText = "Kill all enemies in rooms"
            });    }

    private List<WeaponCellSetup> CreateWeaponCell()
    {
        var availableWeapons = _playerManager.GetAvailableWeapons();
        var result = new List<WeaponCellSetup>();
        var selectedWeapon = _playerManager.GetCurrentWeapon();

        foreach (var weapon in availableWeapons)
        {
            var cellSetup = new WeaponCellSetup()
            {
                onWeaponSelect = _playerManager.ChangeCurrentWeapon,
                isWeaponSelected = weapon == selectedWeapon,
                weaponType = weapon,
                weaponSprite = _balance.SpriteConfig.GetWeaponSprite(weapon),
            };

            result.Add(cellSetup);
        }

        return result;
    }

    private List<LevelCellSetup> CreateLevelCell()
    {
        _currentMapLevel = _playerManager.GetCurrentMapLevel();
        
        var result = new List<LevelCellSetup>();

        for (int i = 0; i < MapsConfig.MapsCount; i++)
        {
            var cellState = LevelState.Current;
            
            if (i < _currentMapLevel)
                cellState = LevelState.Passed;
            else if (i > _currentMapLevel)
                cellState = LevelState.NotPassed;
           

            var cell = new LevelCellSetup()
            {
                onClick = OpenLevel,
                number = i + 1,
                state = cellState
            };
            
            result.Add(cell);
        }

        return result;
    }
}