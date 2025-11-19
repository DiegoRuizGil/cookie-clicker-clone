using System;
using System.Collections.Generic;

namespace Cookie_Clicker.Runtime.Modifiers.Domain
{
    public interface IUpgradeStoreView
    {
        void AddUpgrades(IList<UpgradeDisplayData> displayDataList, Action<string> listener);
        void UpdateButtons(float currentCookies);
    }
}