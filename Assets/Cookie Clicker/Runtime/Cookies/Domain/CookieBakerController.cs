using System;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public struct BuildingBuyRequest
    {
        public Building building;
        public int amount;
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
            _cookieView.UpdateStats(_baker.TotalCookies, _baker.Production);
            _baker.Bake(delta);
        }

        private void ConnectStoreView()
        {
            _storeView.Setup(_baker.GetBuildings());
            _storeView.RegisterListener(BuyBuilding);
            _storeView.UpdateButtons();
        }
        
        private void ConnectCookieView()
        {
            _cookieView.UpdateStats(_baker.TotalCookies, _baker.Production);
            _cookieView.RegisterListener(() => _baker.Tap());
        }

        private void BuyBuilding(BuildingBuyRequest request)
        {
            _baker.AddBuilding(request.building, request.amount);
            _storeView.UpdateButtons();
        }
    }
}