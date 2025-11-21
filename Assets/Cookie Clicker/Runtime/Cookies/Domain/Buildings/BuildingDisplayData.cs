using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Domain.Buildings
{
    public struct BuildingDisplayData
    {
        public BuildingVisibility visibility;
        public string name;
        public Sprite icon;
        public Sprite silhouette;
        public float amount;
        public float cost;
        public float purchaseMult;
        public float cpsPer;
        public float totalProduction;
    }
}