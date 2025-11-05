using Cookie_Clicker.Runtime.Cookies.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class BakerBehaviour : MonoBehaviour
    {
        [SerializeField] private ClickableCookie cookie;

        private readonly CookieBaker _baker = new CookieBaker();
        
        private void OnEnable() => cookie.OnClick += Tap;
        private void OnDisable() => cookie.OnClick -= Tap;

        private void Tap()
        {
            _baker.Tap();
            Debug.Log($"Cookies: {_baker.TotalCookies}");
        }
    }
}