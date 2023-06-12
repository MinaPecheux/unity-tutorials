using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tutorial07_PlacingBuildings
{

    [CustomEditor(typeof(BuildingManager))]
    public class BuildingManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BuildingManager script = (BuildingManager)target;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Fixed")))
                script.SetMaterial(PlacementMode.Fixed);
            if (GUILayout.Button(new GUIContent("Valid")))
                script.SetMaterial(PlacementMode.Valid);
            if (GUILayout.Button(new GUIContent("Invalid")))
                script.SetMaterial(PlacementMode.Invalid);
            EditorGUILayout.EndHorizontal();
        }
    }

}
