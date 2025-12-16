using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public class CursorFieldsDrawer : IUpgradeFieldsDrawer
    {
        private BuildingID _bufferCursorID;
        private float _bufferEfficiencyMult = 2;
        private int _bufferCursorCountToUnlock;
        
        public void Draw()
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Effect Settings", EditorStyles.boldLabel);
            _bufferCursorID = EditorGUILayout.ObjectField("Cursor ID", _bufferCursorID, typeof(BuildingID), false) as BuildingID;
            _bufferEfficiencyMult = EditorGUILayout.FloatField("Efficiency Mult", _bufferEfficiencyMult);
            
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Unlock Condition Settings", EditorStyles.boldLabel);
            _bufferCursorCountToUnlock = EditorGUILayout.IntField("Cursor Count", _bufferCursorCountToUnlock);
        }

        public void ApplyChanges(UpgradeConfigWrapper wrapper)
        {
            wrapper.SO.Update();
            wrapper.SO.FindProperty("cursorID").objectReferenceValue = _bufferCursorID;
            wrapper.SO.FindProperty("efficiencyMultiplier").floatValue = _bufferEfficiencyMult;
            wrapper.SO.FindProperty("cursorCountToUnlock").intValue = _bufferCursorCountToUnlock;
            wrapper.SO.ApplyModifiedProperties();
        }

        public void SetBufferValues(UpgradeConfigWrapper wrapper)
        {
            _bufferCursorID = wrapper.SO.FindProperty("cursorID").objectReferenceValue as BuildingID;
            _bufferEfficiencyMult = wrapper.SO.FindProperty("efficiencyMultiplier").floatValue;
            _bufferCursorCountToUnlock = wrapper.SO.FindProperty("cursorCountToUnlock").intValue;
        }
    }
}