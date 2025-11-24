using UnityEngine;

namespace Cookie_Clicker.Runtime.Cookies.Domain.Buildings
{
    public struct BuildingDisplayData
    {
        public BuildingVisibility visibility;
        public string name;
        public Sprite icon;
        public Sprite silhouette;
        public int amount;
        public double cost;
        public float purchaseMult;
        public double cpsPer;
        public double totalProduction;
    }
}