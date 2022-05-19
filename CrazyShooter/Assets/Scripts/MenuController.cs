using System;
using CrayzShooter.Windows;
using CrayzShooter.Enum;
using CrazyShooter.System;
using SMC.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuController : MonoBehaviour
{
    [Inject]
    private WindowsManager _windowsManager;
    [Inject]
    private SceneTransitionSystem _sceneTransitionSystem;

    private void Awake()
    {
        _windowsManager.Open<MenuWindow, MenuWondowSetup>(new MenuWondowSetup 
        {
            onPlay = () => _sceneTransitionSystem.GoToScene(SceneType.FighScene, true, true)
        });;
    }
}
