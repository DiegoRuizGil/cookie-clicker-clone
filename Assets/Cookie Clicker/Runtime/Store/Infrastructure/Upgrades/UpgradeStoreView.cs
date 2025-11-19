using System;
using System.Collections.Generic;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Upgrades
{
    public class UpgradeStoreView : MonoBehaviour, IUpgradeStoreView
    {
        [SerializeField] private UpgradeButton upgradeButtonPrefab;
        
        public List<UpgradeButton> _buttons = new List<UpgradeButton>();
        
        public void AddUpgrades(IList<UpgradeDisplayData> upgrades, Action<string> listener)
        {
            foreach (var upgrade in upgrades)
            {
                var button = Instantiate(upgradeButtonPrefab, transform);
                button.Init(upgrade);
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