using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Domain;

namespace Cookie_Clicker.Runtime.Builders
{
    public class CookieBakerControllerBuilder : IBuilder<CookieBakerController>
    {
        private CookieBaker _baker;
        private List<Building> _buildings;
        private IBuildingStoreView _storeView;
        private ICookieView _cookieView;

        public CookieBakerControllerBuilder WithCookieBaker(CookieBaker baker)
        {
            _baker = baker;
            return this;
        }

        public CookieBakerControllerBuilder WithBuildings(IList<Building> buildings)
        {
            _buildings = new List<Building>(buildings);
            return this;
        }

        public CookieBakerControllerBuilder WithStoreView(IBuildingStoreView storeView)
        {
            _storeView = storeView;
            return this;
        }

        public CookieBakerControllerBuilder WithCookieView(ICookieView cookieView)
        {
            _cookieView = cookieView;
            return this;
        }
        
        public CookieBakerController Build()
        {
            return new CookieBakerController(_baker, _buildings, _storeView, _cookieView);
        }
    }
}