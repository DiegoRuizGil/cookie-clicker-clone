using System;
using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class BakerBehaviour : MonoBehaviour
    {
        [SerializeField] private ClickableCookie cookie;
        [SerializeField] private StatsVisualizer statsVisualizer;
        [SerializeField] private SerializableInterface<IBuildingStoreView> storeView;
        [SerializeField] private List<BuildingConfig> buildingConfigs;
        
        public CookieBaker Baker => _baker;
        private readonly CookieBaker _baker = new CookieBaker();
        
        private CookieBakerController _controller;

        private void Awake()
        {
            foreach (var config in buildingConfigs)
                _baker.AddBuilding(config.Build());

            _controller = new CookieBakerController(_baker, storeView.GetInstance());
        }

        private void Update()
        {
            _baker.Bake(TimeSpan.FromSeconds(Time.deltaTime));
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