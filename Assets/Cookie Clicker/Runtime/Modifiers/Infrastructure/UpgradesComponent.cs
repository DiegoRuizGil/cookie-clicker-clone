using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker;
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
            var upgradesUnlocker = new UpgradesUnlocker(upgrades.Select(upgrade => upgrade.Get()).ToList(), bakery.Baker);
            _controller = new UpgradeController(upgradesUnlocker, storeView.Instance);
        }

        private void Update()
        {
            _controller.Update();
        }
    }
}