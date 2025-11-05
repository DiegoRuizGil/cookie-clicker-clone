using System;
using Cookie_Clicker.Runtime.Cookies.Domain;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class BakerBehaviour : MonoBehaviour
    {
        [SerializeField] private ClickableCookie cookie;
        [SerializeField] private StatsVisualizer statsVisualizer;

        private readonly CookieBaker _baker = new CookieBaker();

        private void Start()
        {
            statsVisualizer.UpdateStats(_baker);
        }

        private void OnEnable() => cookie.OnClick += Tap;
        private void OnDisable() => cookie.OnClick -= Tap;

        private void Tap()
        {
            _baker.Tap();
            statsVisualizer.UpdateStats(_baker);
        }
    }
}