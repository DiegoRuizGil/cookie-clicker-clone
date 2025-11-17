using System;
using System.Collections.Generic;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public interface IBuildingStoreView
    {
        void Setup(List<Building> buildings);
        void UpdateButtonsData(List<BuildingData> buildingsData);
        void UpdateButtonData(string buildingName, BuildingData data);
        void UpdateButtonsInteraction(float currentCookies, PurchaseMode.Type purchaseType);
        void UpdateVisibility(string buildingName, BuildingVisibility visibility);

        void RegisterPurchasedModeListener(Action<PurchaseMode> listener);
        void RegisterButtonClickListener(Action<string> listener);
        void RegisterButtonHoverListeners(Action<string> onEnter, Action onExit);
    }
}