using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.System;
using Enums;
using SMC.Windows;
using UnityEngine;
using Zenject;

public class SplashSceneController : MonoBehaviour
{
  [Inject] private WindowsManager _windowsManager;
  [Inject] private SceneTransitionSystem _sceneTransitionSystem;

  private void Awake()
  {
    _windowsManager.Open<SplashScreenWindow>();
  }

  private void Start()
  {
    _sceneTransitionSystem.GoToScene(Scene.Menu, false, false);
  }
}
