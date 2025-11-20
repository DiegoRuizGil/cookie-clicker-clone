using System;
using System.Collections.Generic;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Upgrades
{
    public class UpgradeStoreView : MonoBehaviour, IUpgradeStoreView, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private UpgradeButton upgradeButtonPrefab;
        [SerializeField] private UpgradeTooltip upgradeTooltip;
        
        [Header("Layout Settings")]
        [SerializeField] private RectTransform panel;
        [SerializeField] private RectTransform content;
        [SerializeField] private float collapsedHeight = 90f;
        [SerializeField] private float minXPos = 1450;
        
        private readonly List<UpgradeButton> _buttons = new List<UpgradeButton>();

        public void AddUpgrades(IList<UpgradeDisplayData> displayDataList, Action<string> listener)
        {
            foreach (var data in displayDataList)
            {
                var button = Instantiate(upgradeButtonPrefab, content);
                button.Init(data, upgradeTooltip, minXPos);
                button.RegisterListener(listener);
                button.OnButtonPressed += _ => _buttons.Remove(button);
                
                _buttons.Add(button);
            }
        }

        public void UpdateButtons(float currentCookies)
        {
            foreach (var button in _buttons)
                button.SetInteraction(currentCookies);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_buttons.Count <= 0) return;
            
            SetHeight(LayoutUtility.GetPreferredHeight(content));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetHeight(collapsedHeight);
        }

        private void SetHeight(float height)
        {
            var size = panel.sizeDelta;
            size.y = height;
            panel.sizeDelta = size;
        }
    }
}