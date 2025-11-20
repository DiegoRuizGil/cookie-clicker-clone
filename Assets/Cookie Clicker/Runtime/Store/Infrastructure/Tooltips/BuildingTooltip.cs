using System;
using System.Collections;
using Cookie_Clicker.Runtime.Cookies.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips
{
    public class BuildingTooltip : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
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
            icon.sprite = data.icon;
            nameText.text = data.name;
            amountText.text = $"Owned: {data.amount}";
            costText.text = data.cost.ToString("#");
            cpsText.text = $"each cursor produces <color=white>{data.cpsPer:F2} cookies</color> per second";
            productionText.text = $"{data.amount} cursors produces <color=white>{data.totalProduction:F2} cookies</color> per second";
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