using System;
using System.Collections.Generic;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Store.Infrastructure.Tooltips;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Upgrades
{
    public class UpgradeStoreView : MonoBehaviour, IUpgradeStoreView
    {
        [SerializeField] private UpgradeButton upgradeButtonPrefab;
        [SerializeField] private UpgradeTooltip upgradeTooltip;
        [SerializeField] private Transform content;
        
        private readonly List<UpgradeButton> _buttons = new List<UpgradeButton>();

        private float _minXPos;
        
        private void Awake()
        {
            var rt = GetComponent<RectTransform>();
            _minXPos = rt.TransformPoint(new Vector3(rt.rect.xMin, 0, 0)).x;
        }

        public void AddUpgrades(IList<UpgradeDisplayData> displayDataList, Action<string> listener)
        {
            foreach (var data in displayDataList)
            {
                var button = Instantiate(upgradeButtonPrefab, content);
                button.Init(data, upgradeTooltip, _minXPos);
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
    }
}