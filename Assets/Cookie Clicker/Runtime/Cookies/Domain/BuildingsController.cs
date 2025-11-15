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
    
    public class BuildingsController
    {
        private readonly CookieBaker _baker;
        private readonly BuildingsProgression _progression;
        private readonly IBuildingStoreView _view;

        public BuildingsController(CookieBaker baker, BuildingsProgression progression, IBuildingStoreView view)
        {
            _baker = baker;
            _progression = progression;
            _view = view;
            
            ConnectView();
        }

        public void Update()
        {
            _view.UpdateButtons(_baker.CurrentCookies);
        }

        private void ConnectView()
        {
            _view.Setup(_baker.GetBuildings());
            _view.RegisterListener(UpdateBuilding);
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
        }
    }
}