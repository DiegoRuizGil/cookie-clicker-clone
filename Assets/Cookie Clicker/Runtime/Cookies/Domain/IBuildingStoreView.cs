using System;
using System.Collections.Generic;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public interface IBuildingStoreView
    {
        void Setup(List<Building> buildings);
        void RegisterListener(Action<BuildingBuyRequest> callback);
        void UpdateButtons();
    }
}