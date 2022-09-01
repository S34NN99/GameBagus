using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

public class ProjectEventTrigger : MonoBehaviour {
    [SerializeField] private TriggerCondition[] conditions;

    [SerializeField] private bool triggerOnce = true;

    [Space]
    [Tooltip("You guys can leave a comment about what the event should do")]
    [SerializeField] private string remarksForTech;

    private bool hasEventFired;

    private void Awake() {
        foreach (var condition in conditions) {
            condition.SubscribeToProperty();
        }
    }

    /// <summary>
    /// Allows the event's trigger to be delayed until the next time this function is called, regardless of when the value changed.
    /// </summary>
    /// <returns></returns>
    public bool GetTrigger() {
        if (hasEventFired && triggerOnce) return false;

        bool allConditionsMet = true;
        foreach (var condition in conditions) {
            if (!condition.ConditionSatisfied) {
                allConditionsMet = false;
                break;
            }
        }

        if (allConditionsMet) {
            hasEventFired = true;
            return true;
        }

        return false;
    }

    [System.Serializable]
    private class TriggerCondition {
        public enum TriggerType {
            Int_Threshold,
            Float_Threshold,
            Int_Compare_Nums,
            Float_Compare_Nums,
            String_Equals,
            Required_Attributes,
        }
        [SerializeField] private TriggerType triggerType;

        [SerializeField] private bool isLatch;

        [SerializeField] private ObservableVariable[] observedVariables;
        [SerializeField] private string comparedValue;

        public bool ConditionSatisfied { get; private set; }

        public void SubscribeToProperty() {
            switch (triggerType) {
                case TriggerType.Int_Threshold:
                    ObservableProperty<int> targetIntProperty = observedVariables[0] as ObservableProperty<int>;
                    targetIntProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
                        if (isLatch && ConditionSatisfied) {
                            return;
                        }
                        ConditionSatisfied = int.TryParse(comparedValue, out int comparedIntVal) && newVal >= comparedIntVal;
                    });

                    break;
                case TriggerType.Float_Threshold:
                    ObservableProperty<float> targetFloatProperty = observedVariables[0] as ObservableProperty<float>;
                    targetFloatProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
                        if (isLatch && ConditionSatisfied) {
                            return;
                        }
                        ConditionSatisfied = float.TryParse(comparedValue, out float comparedFloatVal) && newVal >= comparedFloatVal;
                    });
                    break;
                case TriggerType.String_Equals:
                    ObservableProperty<string> targetStringProperty = observedVariables[0] as ObservableProperty<string>;
                    targetStringProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
                        if (isLatch && ConditionSatisfied) {
                            return;
                        }
                        ConditionSatisfied = newVal == comparedValue;
                    });
                    break;
                case TriggerType.Required_Attributes:
                    HashSetStringProperty hashSetStringProperty = observedVariables[0] as HashSetStringProperty;
                    hashSetStringProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
                        if (isLatch && ConditionSatisfied) {
                            return;
                        }
                        ConditionSatisfied = hashSetStringProperty.GetValueAsText() == comparedValue;
                    });
                    break;
            }
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TriggerCondition))]
    private class TriggerConditionDrawer : PropertyDrawer {
        private const float PropertyRectHeight = 18;
        private const float PropertyRectHeightWithMargins = 20;

        private bool showContents;
        private bool floatPropClamp01;

        private int propertiesDrawn;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position.height = PropertyRectHeight;
            showContents = EditorGUI.Foldout(position, showContents, label);

            if (showContents) {
                EditorGUI.indentLevel++;

                SerializedProperty triggerTypeProp = property.FindPropertyRelative("triggerType");
                SerializedProperty isLatchProp = property.FindPropertyRelative("isLatch");
                SerializedProperty observedVariableProp = property.FindPropertyRelative("observedVariable");

                GUIContent comparedValueLabel = new GUIContent("Compared");
                SerializedProperty comparedValueProperty = property.FindPropertyRelative("comparedValue");

                position.y += PropertyRectHeightWithMargins;
                EditorGUI.PropertyField(position, triggerTypeProp, new GUIContent("Trigger Type"));

                position.y += PropertyRectHeightWithMargins;
                EditorGUI.PropertyField(position, isLatchProp, new GUIContent("Is Latch", "If true, when the condition is satisfied once, it remains satisfied forever."));

                position.y += PropertyRectHeightWithMargins * 1.5f;
                Object firstObservedProp = observedVariableProp.GetArrayElementAtIndex(0).objectReferenceValue;
                switch ((TriggerCondition.TriggerType)triggerTypeProp.enumValueIndex) {
                    case TriggerCondition.TriggerType.Int_Threshold:
                        if (firstObservedProp is not ObservableProperty<int>) {
                            observedVariableProp.GetArrayElementAtIndex(0).objectReferenceValue = null;
                        }
                        EditorGUI.PropertyField(position, observedVariableProp.GetArrayElementAtIndex(0), new GUIContent("Property 1"));

                        position.y += PropertyRectHeightWithMargins;
                        int.TryParse(comparedValueProperty.stringValue, out int intVal);
                        comparedValueProperty.stringValue = EditorGUI.IntField(position, comparedValueLabel, intVal).ToString();

                        propertiesDrawn = 1;
                        break;
                    case TriggerCondition.TriggerType.Float_Threshold:
                        if (firstObservedProp is not ObservableProperty<float>) {
                            observedVariableProp.GetArrayElementAtIndex(0).objectReferenceValue = null;
                        }
                        EditorGUI.PropertyField(position, observedVariableProp.GetArrayElementAtIndex(0), new GUIContent("Property 1"));

                        position.y += PropertyRectHeightWithMargins;
                        floatPropClamp01 = EditorGUI.Toggle(position, "Clamp 01", floatPropClamp01);

                        position.y += PropertyRectHeightWithMargins;
                        float.TryParse(comparedValueProperty.stringValue, out float floatVal);
                        if (floatPropClamp01) {
                            comparedValueProperty.stringValue = EditorGUI.FloatField(position, comparedValueLabel, floatVal).ToString();
                        } else {
                            comparedValueProperty.stringValue = EditorGUI.Slider(position, comparedValueLabel, Mathf.Clamp01(floatVal), 0, 1).ToString();
                        }

                        propertiesDrawn = 2;
                        break;
                    case TriggerCondition.TriggerType.Float_Compare_Nums:
                        if (firstObservedProp is ObservableProperty<float>) {

                        } else if (firstObservedProp is ObservableProperty<int>) {

                        } else {
                            observedVariableProp.GetArrayElementAtIndex(0).objectReferenceValue = null;
                        }

                        propertiesDrawn = 2;
                        break;
                    case TriggerCondition.TriggerType.String_Equals:
                        if (firstObservedProp is not ObservableProperty<string>) {
                            observedVariableProp.GetArrayElementAtIndex(0).objectReferenceValue = null;
                        }
                        EditorGUI.PropertyField(position, observedVariableProp.GetArrayElementAtIndex(0), new GUIContent("Property 1"));

                        position.y += PropertyRectHeightWithMargins;
                        comparedValueProperty.stringValue = EditorGUI.TextField(position, comparedValueLabel, comparedValueProperty.stringValue);

                        propertiesDrawn = 1;
                        break;
                    case TriggerCondition.TriggerType.Required_Attributes:
                        if (firstObservedProp is not HashSetStringProperty) {
                            observedVariableProp.GetArrayElementAtIndex(0).objectReferenceValue = null;
                        }
                        EditorGUI.PropertyField(position, observedVariableProp.GetArrayElementAtIndex(0), new GUIContent("Property 1"));

                        position.y += PropertyRectHeightWithMargins;
                        comparedValueProperty.stringValue = EditorGUI.TextField(position, comparedValueLabel, comparedValueProperty.stringValue);

                        propertiesDrawn = 1;
                        break;
                    default:
                        break;
                }

                //bool doesObservedVariableMatchTriggerType = (TriggerCondition.TriggerType)triggerTypeProp.enumValueIndex switch {
                //    TriggerCondition.TriggerType.Int_Value_Threshold => firstObservedProp is ObservableProperty<int>,
                //    TriggerCondition.TriggerType.Float_Value_Threshold => firstObservedProp is ObservableProperty<float>,
                //    TriggerCondition.TriggerType.Float_Value_Threshold01 => firstObservedProp is ObservableProperty<float>,
                //    TriggerCondition.TriggerType.String_Equals => firstObservedProp is ObservableProperty<string>,
                //    TriggerCondition.TriggerType.Required_Attributes => firstObservedProp is HashSetStringProperty,
                //    _ => false,
                //};
                //if (!doesObservedVariableMatchTriggerType) {
                //    observedVariableProp.GetArrayElementAtIndex(0).objectReferenceValue = null;
                //}
                //EditorGUI.PropertyField(position, observedVariableProp.GetArrayElementAtIndex(0), new GUIContent("Property 1"));

                //position.y += PropertyRectHeightWithMargins;
                //switch ((TriggerCondition.TriggerType)triggerTypeProp.enumValueIndex) {
                //    case TriggerCondition.TriggerType.Int_Value_Threshold:
                //        int.TryParse(comparedValueProperty.stringValue, out int intVal);

                //        comparedValueProperty.stringValue = EditorGUI.IntField(position, comparedValueLabel, intVal).ToString();
                //        break;
                //    case TriggerCondition.TriggerType.Float_Value_Threshold:
                //        float.TryParse(comparedValueProperty.stringValue, out float floatVal);

                //        comparedValueProperty.stringValue = EditorGUI.FloatField(position, comparedValueLabel, floatVal).ToString();
                //        break;
                //    case TriggerCondition.TriggerType.Float_Value_Threshold01:
                //        float.TryParse(comparedValueProperty.stringValue, out float float01Val);
                //        float01Val = Mathf.Clamp01(float01Val);

                //        comparedValueProperty.stringValue = EditorGUI.Slider(position, comparedValueLabel, float01Val, 0, 1).ToString();
                //        break;
                //    case TriggerCondition.TriggerType.String_Equals:
                //        comparedValueProperty.stringValue = EditorGUI.TextField(position, comparedValueLabel, comparedValueProperty.stringValue);
                //        break;
                //    case TriggerCondition.TriggerType.Required_Attributes:
                //        comparedValueProperty.stringValue = EditorGUI.TextField(position, comparedValueLabel, comparedValueProperty.stringValue);
                //        break;
                //}

                EditorGUI.indentLevel--;
            }


            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return showContents ? PropertyRectHeightWithMargins * (propertiesDrawn + 4.5f) : base.GetPropertyHeight(property, label);
        }
    }
#endif
}
