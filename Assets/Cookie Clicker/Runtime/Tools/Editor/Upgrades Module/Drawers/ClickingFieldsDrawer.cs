using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor.Upgrades_Module.Drawers
{
    public class ClickingFieldsDrawer : IUpgradeFieldsDrawer
    {
        private float _bufferMultiplier = 0.01f;
        private double _bufferHandMadeCookiesToUnlock;
        
        public void Draw()
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Effect Settings", EditorStyles.boldLabel);
            _bufferMultiplier = EditorGUILayout.Slider("Buffer Multiplier", _bufferMultiplier, 0.0f, 1.0f);
            
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Unlock Condition Settings", EditorStyles.boldLabel);
            _bufferHandMadeCookiesToUnlock =
                EditorGUILayout.DoubleField("Hand Made Cookies", _bufferHandMadeCookiesToUnlock);
        }

        public void ApplyChanges(UpgradeConfigWrapper wrapper)
        {
            wrapper.SO.Update();
            wrapper.SO.FindProperty("multiplier").floatValue = _bufferMultiplier;
            wrapper.SO.FindProperty("handMadeCookiesToUnlock").doubleValue = _bufferHandMadeCookiesToUnlock;
            wrapper.SO.ApplyModifiedProperties();
        }

        public void SetBufferValues(UpgradeConfigWrapper wrapper)
        {
            _bufferMultiplier = wrapper.SO.FindProperty("multiplier").floatValue;
            _bufferHandMadeCookiesToUnlock = wrapper.SO.FindProperty("handMadeCookiesToUnlock").doubleValue;
        }
    }
}