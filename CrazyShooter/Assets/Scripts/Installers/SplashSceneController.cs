using CrazyShooter.System;
using CrazyShooter.Enum;
using SMC.Windows;
using UnityEngine;
using Zenject;

public class SplashSceneController : MonoBehaviour
{
    [Inject] private WindowsManager _windowsManager;
    [Inject] private SceneTransitionSystem _sceneTransitionSystem;

    private void Awake()
    {
        _windowsManager.Open<LoadWindow, LoadWindowSetup>(new LoadWindowSetup()
        {
            onLoadAction = () => _sceneTransitionSystem.GoToScene(SceneType.Menu,
                true,true,false)
        });
    }
}