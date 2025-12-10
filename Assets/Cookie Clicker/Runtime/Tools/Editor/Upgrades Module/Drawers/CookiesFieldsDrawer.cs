using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public class CookiesFieldsDrawer : IUpgradeFieldsDrawer
    {
        private float _bufferMultiplier = 0.2f;
        private double _bufferCookiesBakedToUnlock;
        
        public void Draw()
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Effect Settings", EditorStyles.boldLabel);
            _bufferMultiplier = EditorGUILayout.Slider("Multiplier", _bufferMultiplier, 0f, 1f);
            
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Unlock Condition Settings", EditorStyles.boldLabel);
            _bufferCookiesBakedToUnlock = EditorGUILayout.DoubleField("Cookies Baked", _bufferCookiesBakedToUnlock);
        }

        public void ApplyChanges(UpgradeConfigWrapper wrapper)
        {
            wrapper.SO.Update();
            wrapper.SO.FindProperty("multiplier").floatValue = _bufferMultiplier;
            wrapper.SO.FindProperty("cookiesBakedToUnlock").doubleValue = _bufferCookiesBakedToUnlock;
            wrapper.SO.ApplyModifiedProperties();
        }

        public void SetBufferValues(UpgradeConfigWrapper wrapper)
        {
            _bufferMultiplier = wrapper.SO.FindProperty("multiplier").floatValue;
            _bufferCookiesBakedToUnlock = wrapper.SO.FindProperty("cookiesBakedToUnlock").doubleValue;
        }
    }
}