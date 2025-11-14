using System;
using System.Collections.Generic;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public struct BuildingUpdateRequest
    {
        public enum Mode { Buy, Sell }
        
        public Building building;
        public int amount;
        public Mode mode;
        public float cost;
    }
    
    public class CookieBakerController
    {
        public CookieBaker Baker { get; }
        private readonly IBuildingStoreView _storeView;
        private readonly ICookieView _cookieView;
        private readonly List<Building> _buildings;

        public CookieBakerController(CookieBaker baker, List<Building> buildings, IBuildingStoreView storeView, ICookieView cookieView)
        {
            Baker = baker;
            _buildings = buildings;
            _storeView = storeView;
            _cookieView = cookieView;

            ConnectStoreView();
            ConnectCookieView();
        }

        public void Update(TimeSpan delta)
        {
            Baker.Bake(delta);
            
            _cookieView.UpdateStats(Baker.CurrentCookies, Baker.Production);
            _storeView.UpdateButtons(Baker.CurrentCookies);
        }

        private void ConnectStoreView()
        {
            _storeView.Setup(Baker.GetBuildings());
            _storeView.RegisterListener(UpdateBuilding);
        }
        
        private void ConnectCookieView()
        {
            _cookieView.UpdateStats(Baker.CurrentCookies, Baker.Production);
            _cookieView.RegisterListener(() => Baker.Tap());
        }

        private void UpdateBuilding(BuildingUpdateRequest request)
        {
            switch (request.mode)
            {
                case BuildingUpdateRequest.Mode.Buy:
                    Baker.AddBuilding(request.building, request.amount);
                    Baker.CurrentCookies -= request.cost;
                    break;
                case BuildingUpdateRequest.Mode.Sell:
                    Baker.RemoveBuilding(request.building.name, request.amount);
                    Baker.CurrentCookies += request.cost;
                    break;
            }
        }
    }
}