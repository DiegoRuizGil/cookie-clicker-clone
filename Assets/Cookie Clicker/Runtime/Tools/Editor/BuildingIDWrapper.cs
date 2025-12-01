using Cookie_Clicker.Runtime.Cookies.Infrastructure.Buildings;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor
{
    public class BuildingIDWrapper
    {
        public BuildingID ID { get; private set; }
        public SerializedObject SO { get; private set; }
        public SerializedProperty PropName { get; set; }
        

        public BuildingIDWrapper()
        {
            Reset();
        }

        public void Reset()
        {
            ID = ScriptableObject.CreateInstance<BuildingID>();

            SO = new SerializedObject(ID);
            PropName = SO.FindProperty("buildingName");
        }
    }
}