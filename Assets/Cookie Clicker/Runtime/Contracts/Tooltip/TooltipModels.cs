using UnityEngine;

namespace Cookie_Clicker.Runtime.Contracts.Tooltip
{
    public struct BuildingTooltipData
    {
        public Sprite icon;
        public string name;
        public int amount;
        public float cost;
        public float cpsPer;
        public float totalProduction;
        public float totalBaked;
    }

    public struct UpgradeTooltipData
    {
        public Sprite icon;
        public string name;
        public int cost;
        public string description;
    }
}