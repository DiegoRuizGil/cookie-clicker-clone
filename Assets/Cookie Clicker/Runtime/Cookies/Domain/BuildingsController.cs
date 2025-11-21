using System.Collections.Generic;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public struct PurchaseMode
    {
        public enum Type { Buy, Sell }

        public Type type;
        public int multiplier;
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
            ConnectToProgression();
        }

        public void Update()
        {
            _progression.Update(_baker.TotalCookies);
            if (_purchaseMode.type == PurchaseMode.Type.Buy)
                _view.UpdateButtonsInteraction(_baker.CurrentCookies, _purchaseMode.type);
        }

        private void ConnectView()
        {
            _view.Setup(GetDisplayDataList());
            
            _view.RegisterButtonClickListener(UpdateBuilding);
            _view.RegisterPurchasedModeListener(UpdatePurchaseMode);
        }

        private void ConnectToProgression()
        {
            _progression.OnBuildingVisibilityChanged +=
                data => _view.UpdateVisibility(data.building.name, data.visibility);
            
            _progression.Init();
        }
        
        private void UpdateBuilding(string buildingName)
        {
            switch (_purchaseMode.type)
            {
                case PurchaseMode.Type.Buy:
                    _baker.CurrentCookies -= GetCost(buildingName);
                    _baker.AddBuilding(buildingName, _purchaseMode.multiplier);
                    break;
                case PurchaseMode.Type.Sell:
                    _baker.CurrentCookies += GetCost(buildingName);
                    _baker.RemoveBuilding(buildingName, _purchaseMode.multiplier);
                    break;
            }
            
            _view.UpdateButtonData(buildingName, GetDisplayData(buildingName));
        }

        private void UpdatePurchaseMode(PurchaseMode mode)
        {
            _purchaseMode = mode;
            _view.UpdateButtonsData(GetDisplayDataList());
            _view.UpdateButtonsInteraction(_baker.CurrentCookies, _purchaseMode.type);
        }

        private List<BuildingDisplayData> GetDisplayDataList()
        {
            var buildingsData = new List<BuildingDisplayData>();
            foreach (var building in _baker.GetBuildings())
                buildingsData.Add(GetDisplayData(building));
            return buildingsData;
        }

        private BuildingDisplayData GetDisplayData(string buildingName)
        {
            return GetDisplayData(_baker.FindBuilding(buildingName));
        }
        
        private BuildingDisplayData GetDisplayData(Building building)
        {
            return new BuildingDisplayData
            {
                visibility = _progression.GetVisibility(building.name),
                name = building.name,
                icon = building.icon,
                silhouette = building.iconSilhouette,
                amount = building.Amount,
                cost = GetCost(building),
                purchaseMult = _purchaseMode.multiplier,
                cpsPer = building.cps.Value,
                totalProduction = building.Production,
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