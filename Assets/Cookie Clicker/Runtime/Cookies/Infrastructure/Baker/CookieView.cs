using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using TMPro;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class CookieView : MonoBehaviour, ICookieView
    {
        [SerializeField] private ClickableCookie cookie;
        [SerializeField] private TextMeshProUGUI totalCookiesText;
        [SerializeField] private TextMeshProUGUI cpsText;
        
        public void UpdateStats(float totalCookies, float cps)
        {
            totalCookiesText.text = totalCookies.ToString("# 'COOKIES'");
            cpsText.text = cps.ToString("'per second:' #");
        }

        public void RegisterListener(Action callback) => cookie.OnClick += callback;
    }
}