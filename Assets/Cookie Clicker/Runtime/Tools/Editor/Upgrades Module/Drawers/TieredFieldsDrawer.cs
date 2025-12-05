using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public class TieredFieldsDrawer : IUpgradeFieldsDrawer
    {
        private BuildingID _bufferBuilding;
        private float _bufferEfficiencyMult = 2;
        private int _bufferBuildingCountToUnlock;
        
        public void Draw()
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Effect Settings", EditorStyles.boldLabel);
            _bufferBuilding = EditorGUILayout.ObjectField("Building ID", _bufferBuilding, typeof(BuildingID), false) as BuildingID;
            _bufferEfficiencyMult = EditorGUILayout.FloatField("Efficiency Mult", _bufferEfficiencyMult);
            
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Unlock Condition Settings", EditorStyles.boldLabel);
            _bufferBuildingCountToUnlock = EditorGUILayout.IntField("Building Count To Unlock", _bufferBuildingCountToUnlock);
        }

        public void ApplyChanges(UpgradeConfigWrapper wrapper)
        {
            
        }

        public void RevertChanges(UpgradeConfigWrapper wrapper)
        {
            
        }

        public void SetBufferValues(UpgradeConfigWrapper wrapper)
        {
            _bufferBuilding = wrapper.SO.FindProperty("buildingID").objectReferenceValue as BuildingID;
            _bufferEfficiencyMult = wrapper.SO.FindProperty("efficiencyMult").floatValue;
            _bufferBuildingCountToUnlock = wrapper.SO.FindProperty("buildingCountToUnlock").intValue;
        }
    }
}