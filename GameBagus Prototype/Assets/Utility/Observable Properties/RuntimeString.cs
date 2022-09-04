using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class RuntimeStringAttribute : PropertyAttribute {
    public int lineCount;

    public RuntimeStringAttribute(int lineCount = 1) {
        this.lineCount = lineCount <= 0 ? 1 : lineCount;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RuntimeStringAttribute))]
public class RuntimeStringAttributeDrawer : PropertyDrawer {
    private const float PropertyRectHeight = 18;
    private const float PropertyRectHeightWithMargins = 20;
    private const float Spacing = PropertyRectHeightWithMargins / 2;

    private bool isCorrectDataType;
    private float totalHeight;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        RuntimeStringAttribute runtimeStringAttribute = attribute as RuntimeStringAttribute;

        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.String) {
            isCorrectDataType = true;
            float textHeight = PropertyRectHeightWithMargins + (runtimeStringAttribute.lineCount - 1) * PropertyRectHeight;

            position.height = PropertyRectHeightWithMargins;
            EditorGUI.LabelField(position, property.displayName);

            position.y += position.height;
            EditorGUI.indentLevel++;
            EditorGUI.LabelField(position, "Input");

            position.y += position.height;
            position.height = textHeight;
            property.stringValue = EditorGUI.TextArea(position, property.stringValue, EditorStyles.textArea);

            position.y += position.height;
            position.height = PropertyRectHeightWithMargins;
            EditorGUI.LabelField(position, "Output");

            position.y += position.height;
            position.height = textHeight;
            EditorGUI.TextArea(position, ObservableVariable.ConvertToRuntimeText(property.stringValue), EditorStyles.textArea);

            totalHeight = position.height * 2 + PropertyRectHeightWithMargins * 4;
            EditorGUI.indentLevel--;
        } else {
            // for the property itself
            isCorrectDataType = false;

            EditorGUI.LabelField(position, label.text, "Set the field data type to string");
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return isCorrectDataType ? totalHeight : base.GetPropertyHeight(property, label);
    }
}
#endif