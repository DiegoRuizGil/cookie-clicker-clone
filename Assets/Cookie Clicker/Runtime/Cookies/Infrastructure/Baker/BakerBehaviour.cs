using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class BakerBehaviour : MonoBehaviour
    {
        [SerializeField] private ClickableCookie cookie;
        [SerializeField] private StatsVisualizer statsVisualizer;

        public CookieBaker Baker => _baker;
        private readonly CookieBaker _baker = new CookieBaker();

        private void Update()
        {
            _baker.PassTime(TimeSpan.FromSeconds(Time.deltaTime));
            statsVisualizer.UpdateStats(_baker);
        }

        private void OnEnable() => cookie.OnClick += Tap;
        private void OnDisable() => cookie.OnClick -= Tap;

        private void Tap()
        {
            _baker.Tap();
        }
    }
}