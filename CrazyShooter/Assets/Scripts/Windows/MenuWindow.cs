using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.UI;
using SMC.Windows;
using UnityEngine;

namespace CrayzShooter.Windows
{
    public class MenuWindow : Window<MenuWondowSetup>
    {
        [SerializeField] private CustomButton playButton;
        public override void Setup(MenuWondowSetup setup)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(() => setup.onPlay());
        }
    }

    public class MenuWondowSetup : WindowSetup
    {
        public Action onPlay;
    }

}