using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CrazyShooter.UI
{
    public class CustomButton : Button
    {
        [SerializeField]
        private Transform container;
        [SerializeField]
        private TMP_Text textObject;
        [SerializeField]
        private bool disableParentScroll = true;
        [Tooltip("Сделано для того, чтобы при закрытии окна не выдавался варнинг от дутвина")]
        [SerializeField]
        private bool isCloseButton;
        [SerializeField]
        private bool noBounceAnimation;

        public string text
        {
            get => textObject?.text;
            set
            {
                if (textObject != null)
                    textObject.text = value;
            }
        }

        private bool _isDragging;
        private bool _isPointerInside;
        private bool _isPressedDown;
        private Sequence _pointerDownSequence;
        private Sequence _pointerUpSequence;
        private ScrollRect _parentScroll;

        protected override void Start()
        {
            base.Start();
            
            _parentScroll = GetComponentInParent<ScrollRect>();
        }
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            
            eventData?.Use();
            _isDragging = !disableParentScroll && eventData != null && eventData.dragging;
            ReleaseClick();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
            eventData.Use();
            
            if (interactable && !_isPressedDown)
                StartClick();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            
            eventData.Use();
            _isPointerInside = true;
            
            if (_isPressedDown)
                PlayPointerDownAnimation();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            
            eventData.Use();
            _isPointerInside = false;
            PlayPointerUpAnimation();
        }
        
        protected virtual void StartClick()
        {
            _isPressedDown = true;
            DisableParentScroll();
            PlayPointerDownAnimation();
        }

        protected virtual void PlayPointerDownAnimation()
        {
            if (noBounceAnimation)
                return;
            
            _pointerUpSequence?.Kill(true);
            _pointerUpSequence = null;
            
            _pointerDownSequence = DOTween.Sequence();
            _pointerDownSequence.Append(container.DOScale(0.9f, 0.1f));
            _pointerDownSequence.Play();
        }

        protected virtual void ReleaseClick()
        {
            _isPressedDown = false;
            PlayPointerUpAnimation();
            
            if (!_isDragging && _isPointerInside)
                OnClickDetected();
            
            EnableParentScroll();
        }

        protected virtual void PlayPointerUpAnimation()
        {
            if (noBounceAnimation)
                return;
            
            _pointerUpSequence = DOTween.Sequence();
            _pointerUpSequence.Append(container.DOScale(1f, 0.1f));
            _pointerUpSequence.Play();
        }
        
        protected virtual void OnClickDetected()
        {
            if (!interactable)
                return;

            if (isCloseButton)
            {
                _pointerDownSequence?.Kill();
                _pointerUpSequence?.Kill(true);
            }

            // todo vfx and sound
            
            // if (_playVFXOnClick)
            //     _onClickVFX.Play();
            //
            // if (!noClickSound)
            //     _audioController.PlaySound(Sounds.ButtonClick);
        }
        
        private void DisableParentScroll()
        {
            if (!disableParentScroll)
                return;
            
            if (_parentScroll == null)
                return;
            
            _parentScroll.enabled = false;
        }

        private void EnableParentScroll()
        {
            if (!disableParentScroll)
                return;
            
            if (_parentScroll == null)
                return;
            
            _parentScroll.enabled = true;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
    
}
