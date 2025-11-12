using System.Collections.Generic;
using Cookie_Clicker.Runtime.Modifiers.Domain.Upgrades;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public interface IUpgradeStoreView
    {
        void AddUpgrades(IList<Upgrade> upgrades);
    }
}