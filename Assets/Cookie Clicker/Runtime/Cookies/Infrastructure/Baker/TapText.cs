using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class TapText : MonoBehaviour
    {
        [Header("Tween Settings")]
        [SerializeField] private float distance;
        [SerializeField] private float duration;
        
        private TextMeshProUGUI _text;
        private event Action<TapText> KillAction = delegate { }; 

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void Init(double amount, Vector3 position, Action<TapText> killAction)
        {
            transform.position = position;
            KillAction = killAction;
            
            _text.text = "+" + StringUtils.FormatNumber(amount);
            _text.color = Color.white;
            _text.alpha = 1f;
            
            transform.DOKill();
            var sequence = DOTween.Sequence()
                .Append(transform.DOLocalMoveY(transform.position.y + distance, duration))
                .Join(DOTween.ToAlpha(() => _text.color, color => _text.color = color, 0f, duration))
                .AppendCallback(() => KillAction.Invoke(this));
            sequence.Play();
        }
    }
}