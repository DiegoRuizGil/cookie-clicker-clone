namespace Cookie_Clicker.Runtime.Cookies.Domain
{
    public class CookieController
    {
        private readonly CookieBaker _baker;
        private readonly ICookieView _view;

        public CookieController(CookieBaker baker, ICookieView view)
        {
            _baker = baker;
            _view = view;

            ConnectView();
        }

        public void Update()
        {
            _view.UpdateStats(_baker.CurrentCookies, _baker.Production);
        }
        
        private void ConnectView()
        {
            _view.UpdateStats(_baker.CurrentCookies, _baker.Production);
            _view.RegisterListener(() => _baker.Tap());
        }
    }
}