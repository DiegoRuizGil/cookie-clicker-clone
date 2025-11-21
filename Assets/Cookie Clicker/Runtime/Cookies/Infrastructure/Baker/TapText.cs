using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class TapText : MonoBehaviour
    {
        [Header("Tween Settings")]
        [SerializeField] private float distance;
        [SerializeField] private float duration;
        
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void Init(float amount)
        {
            _text.text = "+" + StringUtils.FormatNumber(amount);
            _text.color = Color.white;
            _text.alpha = 1f;
            
            transform.DOKill();
            var sequence = DOTween.Sequence()
                .Append(transform.DOLocalMoveY(transform.position.y + distance, duration))
                .Join(DOTween.ToAlpha(() => _text.color, color => _text.color = color, 0f, duration));
            sequence.Play();
        }
    }
}