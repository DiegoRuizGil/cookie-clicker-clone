using Cookie_Clicker.Runtime.Cookies.Domain;
using TMPro;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class StatsVisualizer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI totalCookiesText;
        [SerializeField] private TextMeshProUGUI cpsText;

        public void UpdateStats(CookieBaker baker)
        {
            totalCookiesText.text = $"{baker.TotalCookies} COOKIES";
            cpsText.text = $"per seconds: {baker.Production}";
        }
    }
}