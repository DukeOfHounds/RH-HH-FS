using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Custom Property Drawer for the RestrictedThermalRange struct
[CustomPropertyDrawer(typeof(ThermalModulator.RestrictedThermalRange))]
public class RestrictedRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Access the serialized field "_thermalValue"
        var valueProperty = property.FindPropertyRelative("_thermalValue");

        // Draw the label and the slider
        EditorGUI.BeginProperty(position, label, property);
        float newValue = EditorGUI.Slider(position, label, valueProperty.floatValue, -1f, 1f);

        // Validate the value and apply it
        valueProperty.floatValue = Mathf.Clamp(newValue, -1f, 1f);

        EditorGUI.EndProperty();
    }
}
