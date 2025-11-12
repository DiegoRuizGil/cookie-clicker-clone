using System.Collections.Generic;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public interface IUpgradeStoreView
    {
        void AddUpgrades(IList<Upgrade> upgrades);
    }
}