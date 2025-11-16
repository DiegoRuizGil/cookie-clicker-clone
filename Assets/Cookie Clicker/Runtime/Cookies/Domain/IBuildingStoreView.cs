using System;
using System.Collections.Generic;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public interface IBuildingStoreView
    {
        void Setup(List<Building> buildings);
        void UpdateButtonsData(List<BuildingData> buildingsData);
        void UpdateButtonsInteraction(float currentCookies, PurchaseMode.Type purchaseType);

        void RegisterPurchasedModeListener(Action<PurchaseMode> listener);
        void RegisterButtonClickListener(Action<string> listener);
    }
}