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

        public CookieBakerController(CookieBaker baker, IBuildingStoreView storeView)
        {
            _baker = baker;
            _storeView = storeView;

            ConnectStoreView();
        }

        private void ConnectStoreView()
        {
            _storeView.Setup(_baker.GetBuildings());
            _storeView.RegisterListener(BuyBuilding);
            _storeView.UpdateButtons();
        }

        private void BuyBuilding(BuildingBuyRequest request)
        {
            _baker.AddBuilding(request.building, request.amount);
            _storeView.UpdateButtons();
        }
    }
}