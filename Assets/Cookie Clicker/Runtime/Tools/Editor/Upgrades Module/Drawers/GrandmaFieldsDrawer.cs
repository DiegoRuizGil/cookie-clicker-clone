using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public class GrandmaFieldsDrawer : IUpgradeFieldsDrawer
    {
        private BuildingID _bufferGrandmaID;
        private BuildingID _bufferBuildingID;
        private float _bufferGrandmaEffMult = 2;
        private float _bufferBuildingMult = 0.01f;
        private int _bufferGrandmaGroupSize;

        private int _bufferGrandmaCountToUnlock;
        private int _bufferBuildingCountToUnlock;
        
        public void Draw()
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Effect Settings", EditorStyles.boldLabel);
            _bufferGrandmaID = EditorGUILayout.ObjectField("Grandma ID", _bufferGrandmaID, typeof(BuildingID), false) as BuildingID;
            _bufferBuildingID = EditorGUILayout.ObjectField("Building ID", _bufferBuildingID, typeof(BuildingID), false) as BuildingID;
            _bufferGrandmaEffMult = EditorGUILayout.FloatField("Grandma Efficiency Mult", _bufferGrandmaEffMult);
            _bufferBuildingMult = EditorGUILayout.Slider("Building Mult", _bufferBuildingMult, 0f, 1f);
            _bufferGrandmaGroupSize = EditorGUILayout.IntField("Grandma Group Size", _bufferGrandmaGroupSize);
            
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Unlock Condition Settings", EditorStyles.boldLabel);
            _bufferGrandmaCountToUnlock = EditorGUILayout.IntField("Grandma Count", _bufferGrandmaCountToUnlock);
            _bufferBuildingCountToUnlock = EditorGUILayout.IntField("Building Count", _bufferBuildingCountToUnlock);
        }

        public void ApplyChanges(UpgradeConfigWrapper wrapper)
        {
            wrapper.SO.Update();
            wrapper.SO.FindProperty("grandmaID").objectReferenceValue = _bufferGrandmaID;
            wrapper.SO.FindProperty("buildingID").objectReferenceValue = _bufferBuildingID;
            wrapper.SO.FindProperty("grandmaEfficiencyMultiplier").floatValue = _bufferGrandmaEffMult;
            wrapper.SO.FindProperty("buildingMultiplier").floatValue = _bufferBuildingMult;
            wrapper.SO.FindProperty("grandmaGroupSize").intValue = _bufferGrandmaGroupSize;
            wrapper.SO.FindProperty("grandmaCountToUnlock").intValue = _bufferGrandmaCountToUnlock;
            wrapper.SO.FindProperty("buildingCountToUnlock").intValue = _bufferBuildingCountToUnlock;
            wrapper.SO.ApplyModifiedProperties();
        }

        public void SetBufferValues(UpgradeConfigWrapper wrapper)
        {
            _bufferGrandmaID = wrapper.SO.FindProperty("grandmaID").objectReferenceValue as BuildingID;
            _bufferBuildingID = wrapper.SO.FindProperty("buildingID").objectReferenceValue as BuildingID;
            _bufferGrandmaEffMult = wrapper.SO.FindProperty("grandmaEfficiencyMultiplier").floatValue;
            _bufferBuildingMult = wrapper.SO.FindProperty("buildingMultiplier").floatValue;
            _bufferGrandmaGroupSize = wrapper.SO.FindProperty("grandmaGroupSize").intValue;
            _bufferGrandmaCountToUnlock = wrapper.SO.FindProperty("grandmaCountToUnlock").intValue;
            _bufferBuildingCountToUnlock = wrapper.SO.FindProperty("buildingCountToUnlock").intValue;
        }
    }
}