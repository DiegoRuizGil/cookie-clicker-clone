using System;

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
        private readonly CookieBaker _baker;
        private readonly IBuildingStoreView _storeView;
        private readonly ICookieView _cookieView;

        public CookieBakerController(CookieBaker baker, IBuildingStoreView storeView, ICookieView cookieView)
        {
            _baker = baker;
            _storeView = storeView;
            _cookieView = cookieView;

            ConnectStoreView();
            ConnectCookieView();
        }

        public void Update(TimeSpan delta)
        {
            _cookieView.UpdateStats(_baker.CurrentCookies, _baker.Production);
            _baker.Bake(delta);
        }

        private void ConnectStoreView()
        {
            _storeView.Setup(_baker.GetBuildings());
            _storeView.RegisterListener(UpdateBuilding);
            _storeView.UpdateButtons();
        }
        
        private void ConnectCookieView()
        {
            _cookieView.UpdateStats(_baker.CurrentCookies, _baker.Production);
            _cookieView.RegisterListener(() => _baker.Tap());
        }

        private void UpdateBuilding(BuildingUpdateRequest request)
        {
            switch (request.mode)
            {
                case BuildingUpdateRequest.Mode.Buy:
                    _baker.AddBuilding(request.building, request.amount);
                    _baker.CurrentCookies -= request.cost;
                    break;
                case BuildingUpdateRequest.Mode.Sell:
                    _baker.RemoveBuilding(request.building.name, request.amount);
                    _baker.CurrentCookies += request.cost;
                    break;
            }
            _storeView.UpdateButtons();
        }
    }
}