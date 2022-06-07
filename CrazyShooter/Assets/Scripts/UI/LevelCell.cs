using System;
using Codice.Client.GameUI.Explorer;
using CrazyShooter.Enums;
using PolyAndCode.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using NotImplementedException = System.NotImplementedException;

namespace CrazyShooter.UI
{
    public class LevelCell : MonoBehaviour, ICell, IScrollDataItem<LevelCellSetup>
    {
        [SerializeField] private TMP_Text number;
        [SerializeField] private Image selectedView;
        [SerializeField] private CustomButton button;
        [SerializeField] private GameObject blackout;


        public void Setup(LevelCellSetup data, UnityAction onButtonClick)
        {
            blackout.SetActive(false);
            SetState(data.state);
            number.text = data.number.ToString();
            button.onClick.RemoveAllListeners();

            if (data.state != LevelState.NotPassed)
                button.onClick.AddListener(() => data.onClick());
        }

        private void SetState(LevelState state)
        {
            switch (state)
            {
                case LevelState.Current:
                    selectedView.color = Color.green;
                    break;
                case LevelState.Passed:
                    button.interactable = false;
                    blackout.SetActive(true);
                    break;
                case LevelState.NotPassed:
                    selectedView.color = Color.white;
                    break;
                default:
                    Debug.LogError("Threr is no case for level state " + state);
                    break;
            }
        }
        
        public void SetAsSelected(bool selected)
        {
        }
    }

    public class LevelCellSetup
    {
        public LevelState state;
        public int number;
        public Action onClick;
    }
}