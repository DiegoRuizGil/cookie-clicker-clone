using System;
using System.Collections.Generic;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class Bakery : MonoBehaviour
    {
        [SerializeField] private SerializableInterface<IBuildingStoreView> storeView;
        [SerializeField] private SerializableInterface<ICookieView> cookieView;
        [SerializeField] private List<BuildingConfig> buildingConfigs;
        
        private CookieBakerController _controller;

        private void Awake()
        {
            var baker = new CookieBaker();
            foreach (var config in buildingConfigs)
                baker.AddBuilding(config.Build());

            _controller = new CookieBakerController(baker, storeView.GetInstance(), cookieView.GetInstance());
        }

        private void Update()
        {
            _controller.Update(TimeSpan.FromSeconds(Time.deltaTime));
        }
    }
}