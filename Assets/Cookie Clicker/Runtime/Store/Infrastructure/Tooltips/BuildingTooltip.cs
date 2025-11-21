using System;
using System.Collections;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Domain.Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips
{
    public class BuildingTooltip : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Sprite defaultSprite;
        
        [Header("UI elements")]
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI cpsText;
        [SerializeField] private TextMeshProUGUI productionText;

        private Vector2 _offset;

        public void Show(BuildingDisplayData data, Vector2 position)
        {
            _offset = new Vector2(rectTransform.sizeDelta.x / 2, rectTransform.sizeDelta.y * 1 / 4);
            UpdateData(data);
            UpdatePosition(position);
            gameObject.SetActive(true);
        }

        public void UpdateData(BuildingDisplayData data)
        {
            icon.sprite = data.visibility switch
            {
                BuildingVisibility.Unlocked => data.icon,
                _ => defaultSprite
            };
            nameText.text = data.visibility switch
            {
                BuildingVisibility.Unlocked => data.name,
                _ => "???"
            };
            amountText.text = $"Owned: {data.amount}";
            costText.text = StringUtils.FormatNumber(data.cost);
            cpsText.text = data.visibility switch
            {
                BuildingVisibility.Unlocked => $"each cursor produces <color=white>{StringUtils.FormatNumber(data.cpsPer)} cookies</color> per second",
                _ => "..."
            };
            productionText.text = data.visibility switch
            {
                BuildingVisibility.Unlocked => $"{data.amount} cursors produces <color=white>{StringUtils.FormatNumber(data.totalProduction)} cookies</color> per second",
                _ => "..."
            };
        }

        public void UpdatePosition(Vector2 position)
        {
            rectTransform.position = position - _offset;
        }

        public void UpdateCostTextColor(bool canPurchase) => costText.color = canPurchase ? Color.green : Color.red;

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}