using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.UI;
using SMC.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CrazyShooter.Windows
{
    public class FightResultWindow : Window<FightResultWindowSetup>
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private CustomButton button;
        
        public override void Setup(FightResultWindowSetup setup)
        {
            title.text = setup.title;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(setup.onButtonClick);
        }
    }

    public class FightResultWindowSetup : WindowSetup
    {
        public UnityAction onButtonClick;
        public string title;
    }
}
