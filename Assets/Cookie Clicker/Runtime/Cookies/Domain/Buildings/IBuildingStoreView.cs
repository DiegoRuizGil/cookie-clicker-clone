using System;
using System.Collections.Generic;

namespace Cookie_Clicker.Runtime.Cookies.Domain.Buildings
{
    public interface IBuildingStoreView
    {
        void Setup(List<BuildingDisplayData> displayDataList);
        void UpdateButtonsData(List<BuildingDisplayData> buildingsData);
        void UpdateButtonData(string buildingName, BuildingDisplayData displayData);
        void UpdateButtonsInteraction(float currentCookies, PurchaseMode.Type purchaseType);
        void UpdateVisibility(string buildingName, BuildingVisibility visibility);

        void RegisterPurchasedModeListener(Action<PurchaseMode> listener);
        void RegisterButtonClickListener(Action<string> listener);
    }
}