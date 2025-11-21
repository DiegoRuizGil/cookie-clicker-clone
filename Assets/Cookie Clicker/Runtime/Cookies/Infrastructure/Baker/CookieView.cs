using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using TMPro;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class CookieView : MonoBehaviour, ICookieView
    {
        [SerializeField] private ClickableCookie cookie;
        [SerializeField] private CursorsController cursorsController;
        [SerializeField] private TextMeshProUGUI totalCookiesText;
        [SerializeField] private TextMeshProUGUI cpsText;
        
        public void UpdateStats(float totalCookies, float cps)
        {
            totalCookiesText.text = $"{StringUtils.FormatNumber(totalCookies)}\nCOOKIES";
            cpsText.text = $"per second: {StringUtils.FormatNumber(cps, true)}";
        }

        public void AddCursors(int amount) => cursorsController.AddCursors(amount);
        public void RemoveCursors(int amount) => cursorsController.RemoveCursors(amount);

        public void RegisterTapListener(Action listener) => cookie.OnClick += listener;
    }
}