// Adapted from:
// - https://forum.unity.com/threads/draw-a-field-only-if-a-condition-is-met.448855/
// - https://github.com/lordofduct/spacepuppy-unity-framework/blob/master/SpacepuppyBaseEditor/EditorHelper.cs

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CustomAttributes
{

  [CustomPropertyDrawer(typeof(DrawIfAttribute))]
  public class DrawIfPropertyDrawer : PropertyDrawer
  {
    // Reference to the attribute on the property.
    DrawIfAttribute drawIf;

    // Field that is being compared.
    SerializedProperty comparedField;

    // Height of the property.
    private float propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      return propertyHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      // Set the global variables.
      drawIf = attribute as DrawIfAttribute;
      comparedField = property.serializedObject.FindProperty(drawIf.comparedPropertyName);

      // Get the value of the compared field.
      object comparedFieldValue = _GetTargetObjectOfProperty(comparedField);

      // References to the values as numeric types.
      SerializedPropertyType comparedFieldType = comparedField.propertyType;

      bool isNumeric = (
        comparedFieldType == SerializedPropertyType.Integer ||
        comparedFieldType == SerializedPropertyType.Float
      );

      // Only numeric types can use the >, <, <=, >= comparison types
      if (!isNumeric && drawIf.comparisonType != ComparisonType.Equals && drawIf.comparisonType != ComparisonType.NotEqual)
      {
        Debug.LogError(
          $"Field '${drawIf.comparedPropertyName}' of type '{comparedFieldType}' cannot use " +
          $"comparison type: '${drawIf.comparisonType}'. (On object '${property.serializedObject.targetObject.name}')");
        return;
      }

      // Is the condition met? Should the field be drawn?
      bool conditionMet = false;

      // Compare the values to see if the condition is met.
      if (drawIf.comparisonType == ComparisonType.Equals)
        conditionMet = comparedFieldValue.Equals(drawIf.comparedValue);
      else if (drawIf.comparisonType == ComparisonType.Equals)
        conditionMet = !comparedFieldValue.Equals(drawIf.comparedValue);
      else
      {
        // (integer)
        if (comparedFieldType == SerializedPropertyType.Integer)
        {
          int numericComparedValue = (int)drawIf.comparedValue;
          int numericComparedFieldValue = comparedField.intValue;
          switch (drawIf.comparisonType)
          {
            case ComparisonType.GreaterThan:
              conditionMet = numericComparedFieldValue > numericComparedValue;
              break;
            case ComparisonType.SmallerThan:
              conditionMet = numericComparedFieldValue < numericComparedValue;
              break;
            case ComparisonType.GreaterOrEqual:
              conditionMet = numericComparedFieldValue >= numericComparedValue;
              break;
            case ComparisonType.SmallerOrEqual:
              conditionMet = numericComparedFieldValue <= numericComparedValue;
              break;
          }
        }
        // (float)
        if (comparedFieldType == SerializedPropertyType.Float)
        {
          float numericComparedValue = (float)drawIf.comparedValue;
          float numericComparedFieldValue = comparedField.floatValue;
          switch (drawIf.comparisonType)
          {
            case ComparisonType.GreaterThan:
              conditionMet = numericComparedFieldValue > numericComparedValue;
              break;
            case ComparisonType.SmallerThan:
              conditionMet = numericComparedFieldValue < numericComparedValue;
              break;
            case ComparisonType.GreaterOrEqual:
              conditionMet = numericComparedFieldValue >= numericComparedValue;
              break;
            case ComparisonType.SmallerOrEqual:
              conditionMet = numericComparedFieldValue <= numericComparedValue;
              break;
          }
        }
      }

      // The height of the property should be defaulted to the default height.
      propertyHeight = base.GetPropertyHeight(property, label);

      // If the condition is met, simply draw the field. Else...
      if (conditionMet)
      {
        EditorGUI.PropertyField(position, property);
      }
      else
      {
        //...check if the disabling type is read only. If it is, draw it disabled, else, set the height to zero.
        if (drawIf.disablingType == DisablingType.ReadOnly)
        {
          GUI.enabled = false;
          EditorGUI.PropertyField(position, property);
          GUI.enabled = true;
        }
        else
        {
          propertyHeight = 0f;
        }
      }
    }

    //private static object _GetPropertyValue(SerializedProperty property)
    //{
    //  switch (property.propertyType)
    //  {
    //    case SerializedPropertyType.Integer:
    //      return property.intValue;
    //    case SerializedPropertyType.Boolean:
    //      return property.boolValue;
    //    case SerializedPropertyType.Float:
    //      return property.floatValue;
    //    case SerializedPropertyType.String:
    //      return property.stringValue;
    //    case SerializedPropertyType.Color:
    //      return property.colorValue;
    //    case SerializedPropertyType.ObjectReference:
    //      return property.objectReferenceValue;
    //    case SerializedPropertyType.LayerMask:
    //      return property.intValue;
    //    case SerializedPropertyType.Enum:
    //      return property.enumValueIndex;
    //    case SerializedPropertyType.Vector2:
    //      return property.vector2Value;
    //    case SerializedPropertyType.Vector3:
    //      return property.vector3Value;
    //    case SerializedPropertyType.Vector4:
    //      return property.vector4Value;
    //  }
    //}

    private static object _GetTargetObjectOfProperty(SerializedProperty prop)
    {
      if (prop == null) return null;

      var path = prop.propertyPath.Replace(".Array.data[", "[");
      object obj = prop.serializedObject.targetObject;
      var elements = path.Split('.');
      foreach (var element in elements)
      {
        if (element.Contains("["))
        {
          var elementName = element.Substring(0, element.IndexOf("["));
          var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
          obj = _GetValue(obj, elementName, index);
        }
        else
        {
          obj = _GetValue(obj, element);
        }
      }
      return obj;
    }

    private static object _GetValue(object source, string name)
    {
      if (source == null)
        return null;
      var type = source.GetType();

      while (type != null)
      {
        var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (f != null)
          return f.GetValue(source);

        var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        if (p != null)
          return p.GetValue(source, null);

        type = type.BaseType;
      }
      return null;
    }

    private static object _GetValue(object source, string name, int index)
    {
      var enumerable = _GetValue(source, name) as System.Collections.IEnumerable;
      if (enumerable == null) return null;
      var enm = enumerable.GetEnumerator();
      for (int i = 0; i <= index; i++)
      {
        if (!enm.MoveNext()) return null;
      }
      return enm.Current;
    }
  }

}
