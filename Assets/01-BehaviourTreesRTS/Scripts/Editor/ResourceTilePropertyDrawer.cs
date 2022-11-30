#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace T01_BehaviourTreesRTS
{

    [CustomPropertyDrawer(typeof(ResourceTile))]
    public class ResourceTilePropertyDrawer : PropertyDrawer
    {
        private static float _ICON_SIZE = 50f;

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            label.text = label.text.Replace("Element", "Resource");
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            float iconWidth = _ICON_SIZE;
            float valueWidth = position.width - _ICON_SIZE - 5;
            Rect iconRect = new Rect(position.x, position.y, iconWidth, position.height);
            Rect valueRect = new Rect(position.x + iconWidth + 5, position.y, valueWidth, 32);

            Sprite sprite = property.FindPropertyRelative("sprite").objectReferenceValue as Sprite;
            GUI.DrawTexture(iconRect, sprite.texture, ScaleMode.StretchToFill);//, ScaleMode.ScaleToFit);

            // (use GUIContent.none to hide label)
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => _ICON_SIZE;
    }

}

#endif
