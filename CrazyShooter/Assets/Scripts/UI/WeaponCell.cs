using System;
using CrazyShooter.Configs;
using CrazyShooter.Enum;
using PolyAndCode.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace CrazyShooter.UI
{
    public class WeaponCell : MonoBehaviour, ICell, IScrollDataItem<WeaponCellSetup>
    {
        [SerializeField] private TMP_Text weaponName;
        [SerializeField] private Image weaponImage;
        [SerializeField] private CustomButton button;
        [SerializeField] private Image selectedView;

        public void Setup(WeaponCellSetup item, UnityAction onButtonClick)
        {
            SetAsSelected(false);
            weaponName.text = item.weaponType.ToString();
            weaponImage.sprite = item.weaponSprite;
            button.onClick.AddListener(() =>
            {
                onButtonClick?.Invoke();
                SetAsSelected(true);
            });
        }

        public void SetAsSelected(bool selected)
        {
            if (selected)
                selectedView.color = Color.green;
            else
                selectedView.color = Color.white;
        }
    }

    public class WeaponCellSetup
    {
        public Action onclick;
        public WeaponType weaponType;
        public Sprite weaponSprite;
    }
}