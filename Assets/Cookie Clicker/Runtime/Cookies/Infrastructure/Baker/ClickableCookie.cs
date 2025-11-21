using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class ClickableCookie : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Scale Tween Settings")]
        [SerializeField] private float scaleTweenEndScale = 1.15f;
        [SerializeField] private float scaleTweenDuration = 0.1f;
        [SerializeField] private Ease scaleTweenEase = Ease.OutBounce;
        
        [Header("Click Tween Settings")]
        [SerializeField] private float clickTweenPunch = 0.1f;
        [SerializeField] private float clickTweenDuration = 0.1f;
        [SerializeField] private int clickTweenVibrato = 10;
        [SerializeField] private float clickTweenElasticity = 0.5f;
        
        public event Action OnClick = () => { };

        private Tween _scaleTween;
        private Tween _clickTween;
        
        private void Awake()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick.Invoke();
            
            _clickTween?.Kill();
            _clickTween = transform.DOPunchScale(
                new Vector3(clickTweenPunch, clickTweenPunch, 0f),
                clickTweenDuration,
                clickTweenVibrato,
                clickTweenElasticity);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(scaleTweenEndScale, scaleTweenDuration).SetEase(scaleTweenEase);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(1f, scaleTweenDuration).SetEase(scaleTweenEase);
        }
    }
}