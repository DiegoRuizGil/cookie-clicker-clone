using System;

namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CookieController
    {
        private readonly CookieBaker _baker;
        private readonly ICookieView _view;
        private readonly string _cursorBuildingName;

        public CookieController(CookieBaker baker, ICookieView view, string cursorBuildingName)
        {
            _baker = baker;
            _view = view;
            _cursorBuildingName = cursorBuildingName;

            ConnectView();
            _baker.OnBuildingAmountChanged += HandleCursors;
        }

        public void Update()
        {
            _view.UpdateStats(_baker.CurrentCookies, _baker.Production);
        }
        
        private void ConnectView()
        {
            _view.UpdateStats(_baker.CurrentCookies, _baker.Production);
            _view.RegisterTapListener(() => _baker.Tap());
        }
        
        private void HandleCursors(Building building, int amount)
        {
            if (building.name != _cursorBuildingName) return;
            
            if (amount > 0)
                _view.AddCursors(amount);
            else if (amount < 0)
                _view.RemoveCursors(Math.Abs(amount));
        }
    }
}