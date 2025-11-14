using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Cookie_Clicker.Runtime.Builders;
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

        public CookieBaker Baker => _controller.Baker;
        
        private CookieBakerController _controller;

        private void Awake()
        {
            var baker = new CookieBaker();
            baker.SetInitialBuildings(buildingConfigs.Select(config => config.Get()).ToList());

            _controller = A.CookieBakerController
                .WithCookieBaker(baker)
                .WithStoreView(storeView.Instance)
                .WithCookieView(cookieView.Instance)
                .Build();
        }

        private void Update()
        {
            _controller.Update(TimeSpan.FromSeconds(Time.deltaTime));
        }
    }
}