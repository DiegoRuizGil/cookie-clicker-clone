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

        private void Update()
        {
            _baker.PassTime(TimeSpan.FromSeconds(Time.deltaTime));
            statsVisualizer.UpdateStats(_baker);
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
                DebugAddCursorBuilding();
        }

        private void OnEnable() => cookie.OnClick += Tap;
        private void OnDisable() => cookie.OnClick -= Tap;

        private void Tap()
        {
            _baker.Tap();
        }

        private void DebugAddCursorBuilding()
        {
            var cursor = new Building("cursor", 2);
            _baker.AddBuilding(cursor);
        }
    }
}