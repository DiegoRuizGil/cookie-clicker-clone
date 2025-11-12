namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public class UpgradeController
    {
        private readonly UpgradesUnlocker _unlocker;
        private readonly IUpgradeStoreView _storeView;

        public UpgradeController(UpgradesUnlocker unlocker, IUpgradeStoreView storeView)
        {
            _unlocker = unlocker;
            _storeView = storeView;
        }

        public void Update()
        {
            _unlocker.CheckUnlocks();
            
            if (_unlocker.NewUnlocksInLastCheck)
                _storeView.AddUpgrades(_unlocker.LastUpgradesUnlocked);
        }
    }
}