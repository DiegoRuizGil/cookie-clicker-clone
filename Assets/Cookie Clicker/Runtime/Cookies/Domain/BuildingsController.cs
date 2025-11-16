using System;
using System.Collections.Generic;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public struct PurchaseMode
    {
        public enum Type { Buy, Sell }

        public Type type;
        public int multiplier;
    }

    public struct BuildingData
    {
        public float cost;
        public float multiplier;
        public float amount;
    }
    
    public class BuildingsController
    {
        private readonly CookieBaker _baker;
        private readonly BuildingsProgression _progression;
        private readonly IBuildingStoreView _view;
        
        private PurchaseMode _purchaseMode = new PurchaseMode { type = PurchaseMode.Type.Buy, multiplier = 1 };

        public BuildingsController(CookieBaker baker, BuildingsProgression progression, IBuildingStoreView view)
        {
            _baker = baker;
            _progression = progression;
            _view = view;
            
            ConnectView();
        }

        public void Update()
        {
            if (_purchaseMode.type == PurchaseMode.Type.Buy)
                _view.UpdateButtonsInteraction(_baker.CurrentCookies, _purchaseMode.type);
        }

        private void ConnectView()
        {
            _view.Setup(_baker.GetBuildings());
            _view.UpdateButtonsData(GetBuildingsData());
            _view.UpdateButtonsInteraction(_baker.CurrentCookies, _purchaseMode.type);
            
            _view.RegisterButtonClickListener(UpdateBuilding);
            _view.RegisterPurchasedModeListener(UpdatePurchaseMode);
        }
        
        private void UpdateBuilding(string buildingName)
        {
            switch (_purchaseMode.type)
            {
                case PurchaseMode.Type.Buy:
                    _baker.AddBuilding(buildingName, _purchaseMode.multiplier);
                    _baker.CurrentCookies -= GetCost(buildingName);
                    break;
                case PurchaseMode.Type.Sell:
                    _baker.RemoveBuilding(buildingName, _purchaseMode.multiplier);
                    _baker.CurrentCookies += GetCost(buildingName);
                    break;
            }
            
            _view.UpdateButtonsData(GetBuildingsData());
        }

        private void UpdatePurchaseMode(PurchaseMode mode)
        {
            _purchaseMode = mode;
            _view.UpdateButtonsData(GetBuildingsData());
            _view.UpdateButtonsInteraction(_baker.CurrentCookies, _purchaseMode.type);
        }

        private List<BuildingData> GetBuildingsData()
        {
            var buildingsData = new List<BuildingData>();
            foreach (var building in _baker.GetBuildings())
                buildingsData.Add(GetBuildingInfo(building));
            return buildingsData;
        }
        
        private BuildingData GetBuildingInfo(Building building)
        {
            return new BuildingData
            {
                cost = GetCost(building),
                multiplier = _purchaseMode.multiplier,
                amount = building.Amount
            };
        }

        private float GetCost(string buildingName)
        {
            var building = _baker.FindBuilding(buildingName);
            return building == null ? 0f : GetCost(building);
        }

        private float GetCost(Building building)
        {
            return _purchaseMode.type switch
            {
                PurchaseMode.Type.Buy => building.CostOf(_purchaseMode.multiplier),
                PurchaseMode.Type.Sell => building.RefoundOf(_purchaseMode.multiplier),
                _ => 0f
            };
        }
    }
}