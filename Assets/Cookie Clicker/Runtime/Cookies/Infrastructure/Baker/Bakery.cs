using System;
using System.Collections.Generic;
using System.Linq;
using Cookie_Clicker.Runtime.Cookies.Domain;
using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class Bakery : MonoBehaviour
    {
        [SerializeField] private BuildingID cursorID;
        [SerializeField] private SerializableInterface<IBuildingStoreView> storeView;
        [SerializeField] private SerializableInterface<ICookieView> cookieView;
        [SerializeField] private List<BuildingConfig> buildingConfigs;

        public CookieBaker Baker { get; private set; }
        
        private CookieController _cookieController;
        private BuildingsController _buildingsController;

        private void Awake()
        {
            Baker = new CookieBaker();
            Baker.SetBuildings(buildingConfigs.Select(config => config.Get()).ToList());
            
            var progression = new BuildingsProgression(Baker.GetBuildings());
            _buildingsController = new BuildingsController(Baker, progression, storeView.Instance);
            _cookieController = new CookieController(Baker, cookieView.Instance, cursorID);
        }

        private void Update()
        {
            Baker.Bake(TimeSpan.FromSeconds(Time.deltaTime));
            
            _buildingsController.Update();
            _cookieController.Update();
        }
    }
}