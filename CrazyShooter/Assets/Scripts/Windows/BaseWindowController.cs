using System.Collections;
using System.Collections.Generic;
using CrazyShooter.UI;
using SMC.Tools.Attributes;
using SMC.Windows;
using UnityEngine;

namespace CrazyShooter.Windows
{
    public class BaseWindowController : WindowController
    {
        [SerializeField] protected RectTransform safeArea;
        [SerializeField, NotRequiredField] protected CustomButton closeButton;

        protected override void Awake()
        {
            base.Awake();
            UpdateSafeArea();
        }

        protected virtual void OnEnable()
        {
            if (closeButton != null)
                closeButton.onClick.AddListener(Close);
        }

        protected virtual void OnDisable()
        {
            if (closeButton != null)
                closeButton.onClick.RemoveAllListeners();
        }

        private void UpdateSafeArea()
        {
            // copy paste from official unity tutorial
            // https://www.youtube.com/watch?v=PLQ4ywB13eg

            var screenSafeArea = Screen.safeArea;

            var anchorMin = screenSafeArea.position;
            var anchorMax = screenSafeArea.position + screenSafeArea.size;

            var pixelRect = Canvas.pixelRect;

            anchorMin.x /= pixelRect.width;
            anchorMin.y /= pixelRect.height;

            anchorMax.x /= pixelRect.width;
            anchorMax.y /= pixelRect.height;

            safeArea.anchorMin = anchorMin;
            safeArea.anchorMax = anchorMax;
        }
    }
    
}
