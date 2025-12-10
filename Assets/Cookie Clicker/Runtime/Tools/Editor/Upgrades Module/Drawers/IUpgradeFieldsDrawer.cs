namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public interface IUpgradeFieldsDrawer
    {
        void Draw();
        void ApplyChanges(UpgradeConfigWrapper wrapper);
        void SetBufferValues(UpgradeConfigWrapper wrapper);
    }
}