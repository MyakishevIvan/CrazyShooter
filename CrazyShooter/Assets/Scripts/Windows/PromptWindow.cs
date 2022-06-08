using System.Collections;
using System.Collections.Generic;
using CrazyShooter.UI;
using SMC.Windows;
using TMPro;
using UnityEngine;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace CrazyShooter.Windows
{
    public class PromptWindow : Window<PromptWindowSetup>
    {
        [SerializeField] private TMP_Text promprText;
        [SerializeField] private CustomButton okButton;
        [Inject] private WindowsManager _windowsManager;
        public override void Setup(PromptWindowSetup setup)
        {
            promprText.text = setup.pomptText;
            okButton.onClick.RemoveAllListeners();
            okButton.onClick.AddListener(()=> _windowsManager.Close<PromptWindow>());
        }
    }

    public class PromptWindowSetup : WindowSetup
    {
        public string pomptText;
    }
}
