using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public class GeneralFieldsDrawer : IUpgradeFieldsDrawer
    {
        private string _bufferName = "";
        private Sprite _bufferIcon;
        private double _bufferCost;
        private string _bufferDescription = "";
        
        private Vector2 _descriptionScroll;
        
        public void Draw()
        {
            EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
            GUI.SetNextControlName("Name");
            _bufferName = EditorGUILayout.TextField("Name", _bufferName);
            _bufferIcon = EditorGUILayout.ObjectField("Icon", _bufferIcon, typeof(Sprite), false) as Sprite;
            _bufferCost = EditorGUILayout.DoubleField("Cost", _bufferCost);
            EditorGUILayout.LabelField("Description");
            _descriptionScroll = EditorGUILayout.BeginScrollView(_descriptionScroll, GUILayout.Height(60));
            _bufferDescription = EditorGUILayout.TextArea(_bufferDescription, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
        }

        public void ApplyChanges(UpgradeConfigWrapper wrapper)
        {
            
        }

        public void RevertChanges(UpgradeConfigWrapper wrapper)
        {
            
        }

        public void SetBufferValues(UpgradeConfigWrapper wrapper)
        {
            _bufferName = wrapper.PropName.stringValue;
            _bufferIcon = wrapper.PropIcon.objectReferenceValue as Sprite;
            _bufferCost = wrapper.PropCost.doubleValue;
            _bufferDescription = wrapper.PropDescription.stringValue;
        }
    }
}