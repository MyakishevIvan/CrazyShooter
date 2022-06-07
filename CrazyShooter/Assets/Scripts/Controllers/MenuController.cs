using System;
using CrazyShooter.Windows;
using CrazyShooter.Enum;
using CrazyShooter.System;
using SMC.Windows;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        _windowsManager.Open<MenuWindow, MenuWondowSetup>(new MenuWondowSetup
        {
            onPlay = OpenPlayerLevelWindow
        });
        ;
    }

    private void OpenPlayerLevelWindow()
    {
        Action onOpenLevel = () => OpenLevel();
        var levelCellList = new List<LevelCellSetup>();

        for (int i = 0; i < 20; i++)
        {
            var levelState = LevelState.Passed;
            if (i > 7)
                levelState = LevelState.NotPassed;
            else if (i == 7)
                levelState = LevelState.Current;
            var cell = new LevelCellSetup()
            {
                state = levelState,
                number = i+1,
                onClick =  onOpenLevel,
            };
            levelCellList.Add(cell);
        }

        var list = new List<WeaponCellSetup>()
        {
            new WeaponCellSetup
            {
                onclick = null,
                weaponType = WeaponType.Sword,
                weaponSprite = _balance.SpriteConfig.GetWeaponSprite(WeaponType.Sword),
            },
            new WeaponCellSetup
            {
                onclick = null,
                weaponType = WeaponType.Gun,
                weaponSprite = _balance.SpriteConfig.GetWeaponSprite(WeaponType.Gun),
            },
            new WeaponCellSetup
            {
                onclick = null,
                weaponType = WeaponType.SmallGun,
                weaponSprite = _balance.SpriteConfig.GetWeaponSprite(WeaponType.SmallGun),
            }
        };

        var setup = new PlayerLevelWindowSetup()
        {
            weaponScrollDataSource = new ItemsScrollDataSource<WeaponCellSetup>(list),
            levelScrollDataSource = new ItemsScrollDataSource<LevelCellSetup>(levelCellList)
        };

        _windowsManager.Open<PlayerLevelWindow, PlayerLevelWindowSetup>(setup);
        _windowsManager.Close<MenuWindow>();
    }

    private void OpenLevel()
    {
        _sceneTransitionSystem.GoToScene(SceneType.FighScene);
    }
}