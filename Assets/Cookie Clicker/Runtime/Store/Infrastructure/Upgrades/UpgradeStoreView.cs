using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using Cookie_Clicker.Runtime.Modifiers.Domain.Upgrades;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Store.Infrastructure.Upgrades
{
    public class UpgradeStoreView : MonoBehaviour, IUpgradeStoreView
    {
        [SerializeField] private UpgradeButton upgradeButtonPrefab;
        [SerializeField] private Bakery bakery;
        
        public void AddUpgrades(IList<Upgrade> upgrades)
        {
            foreach (var upgrade in upgrades)
            {
                var button = Instantiate(upgradeButtonPrefab, transform);
                button.Init(upgrade, bakery.Baker);
            }
        }
    }
}