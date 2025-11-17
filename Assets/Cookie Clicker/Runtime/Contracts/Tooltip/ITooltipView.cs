namespace Cookie_Clicker.Runtime.Contracts.Tooltip
{
    public interface ITooltipView
    {
        void Show(BuildingTooltipData data);
        void Show(UpgradeTooltipData data);
        void Hide();
    }
}