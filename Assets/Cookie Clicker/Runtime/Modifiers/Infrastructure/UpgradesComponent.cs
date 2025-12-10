using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker;
using Cookie_Clicker.Runtime.CustomAttributes;
using Cookie_Clicker.Runtime.Modifiers.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Modifiers.Infrastructure
{
    public class UpgradesComponent : MonoBehaviour
    {
        [SerializeField] private Bakery bakery;
        [SerializeField] private SerializableInterface<IUpgradeStoreView> storeView;
        [SerializeField] private List<BaseUpgradeConfig> upgrades = new List<BaseUpgradeConfig>();

        private UpgradeController _controller;
        
        private void Awake()
        {
            var upgradeList = upgrades.Where(upgrade => upgrade != null && upgrade.IsValid())
                .Select(upgrade => upgrade.Get()).ToList();
            var upgradesUnlocker = new UpgradesUnlocker(upgradeList, bakery.Baker);
            _controller = new UpgradeController(upgradesUnlocker, bakery.Baker, storeView.Instance);
        }

        private void Update()
        {
            _controller.Update();
        }

        public void LoadUpgrades(List<BaseUpgradeConfig> upgrades) => this.upgrades = upgrades;
    }
}