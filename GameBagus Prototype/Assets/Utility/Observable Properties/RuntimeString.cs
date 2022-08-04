using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class RuntimeStringAttribute : PropertyAttribute {
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RuntimeStringAttribute))]
public class RuntimeStringAttributeDrawer : PropertyDrawer {
    private const float PropertyRectHeight = 18f;
    private const float PropertyRectHeightWithMargins = 20;

    private bool isCorrectDataType;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.String) {
            isCorrectDataType = true;

            position.height = PropertyRectHeight;
            property.stringValue = EditorGUI.TextField(position, label, property.stringValue);

            EditorGUI.indentLevel++;
            position.y += PropertyRectHeightWithMargins;
            EditorGUI.LabelField(position, "->", ObservableVariable.ConvertToRuntimeText(property.stringValue));
            EditorGUI.indentLevel--;
        } else {
            isCorrectDataType = false;

            EditorGUI.LabelField(position, label.text, "Set the field data type to string");
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return isCorrectDataType ? PropertyRectHeightWithMargins * 2 : base.GetPropertyHeight(property, label);
    }
}
#endif