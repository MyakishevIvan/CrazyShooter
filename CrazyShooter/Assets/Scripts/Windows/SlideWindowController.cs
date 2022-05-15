using System;
using CrazyShooter.Enums;
using DG.Tweening;
using UnityEngine;

namespace CrazyShooter.Windows
{
    public class SlideWindowController : BaseWindowController
    {
        private const float AnimDuration = .4f;

        [SerializeField] private RectTransform mainContainer;
        [SerializeField] private WindowSlideDirection direction;

        protected override void AnimateOpen(Action completeCallback)
        {
            var rect = Canvas.pixelRect;

            switch (direction)
            {
                case WindowSlideDirection.TopToBottom:
                    mainContainer.anchoredPosition += new Vector2(0, rect.height);
                    break;

                case WindowSlideDirection.BottomToTop:
                    mainContainer.anchoredPosition += new Vector2(0, -rect.height);
                    break;

                case WindowSlideDirection.LeftToRight:
                    mainContainer.anchoredPosition += new Vector2(-rect.width, 0);
                    break;

                case WindowSlideDirection.RightToLeft:
                    mainContainer.anchoredPosition += new Vector2(rect.width, 0);
                    break;

                default:
                    completeCallback?.Invoke();
                    return;
            }

            var tween = mainContainer.DOLocalMove(Vector3.zero, AnimDuration);
            tween.onComplete = () => completeCallback?.Invoke();
        }

        protected override void AnimateClose(Action completeCallback)
        {
            var rect = Canvas.pixelRect;
            Vector2 vectorTo;

            switch (direction)
            {
                case WindowSlideDirection.TopToBottom:
                    vectorTo = new Vector2(0, rect.height);
                    break;

                case WindowSlideDirection.BottomToTop:
                    vectorTo = new Vector2(0, -rect.height);
                    break;

                case WindowSlideDirection.LeftToRight:
                    vectorTo = new Vector2(-rect.width, 0);
                    break;

                case WindowSlideDirection.RightToLeft:
                    vectorTo = new Vector2(rect.width, 0);
                    break;

                default:
                    completeCallback?.Invoke();
                    return;
            }

            var tween = mainContainer.DOLocalMove(vectorTo, AnimDuration);
            tween.onComplete = () => completeCallback?.Invoke();
        }
    }
}