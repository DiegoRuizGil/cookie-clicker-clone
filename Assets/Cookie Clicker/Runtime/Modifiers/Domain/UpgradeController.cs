using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class UpgradeController
    {
        private readonly UpgradesUnlocker _unlocker;
        private readonly CookieBaker _baker;
        private readonly IUpgradeStoreView _storeView;

        public UpgradeController(UpgradesUnlocker unlocker, CookieBaker baker, IUpgradeStoreView storeView)
        {
            _unlocker = unlocker;
            _baker = baker;
            _storeView = storeView;
        }

        public void Update()
        {
            _unlocker.CheckUnlocks();
            
            if (_unlocker.NewUnlocksInLastCheck)
                _storeView.AddUpgrades(_unlocker.LastUpgradesUnlocked, OnUpgradePurchased);
            _storeView.UpdateButtons(_baker.CurrentCookies);
        }

        private void OnUpgradePurchased(Upgrade upgrade)
        {
            var u = _unlocker.FindUnlockedUpgrade(upgrade.name);
            u?.Apply(_baker);
        }

        private UpgradeDisplayData GetDisplayData(Upgrade upgrade)
        {
            return new UpgradeDisplayData
            {
                name = upgrade.name,
                icon = upgrade.icon,
                cost = upgrade.cost,
                description = upgrade.description
            };
        }
    }
}