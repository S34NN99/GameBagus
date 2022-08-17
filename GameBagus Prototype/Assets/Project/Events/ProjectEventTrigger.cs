using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

public class ProjectEventTrigger : MonoBehaviour {
    [SerializeField] private TriggerCondition[] conditions;

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
        bool allConditionsMet = true;
        foreach (var condition in conditions) {
            if (!condition.ConditionSatisfied) {
                allConditionsMet = false;
                break;
            }
        }

        if (allConditionsMet && !hasEventFired) {
            hasEventFired = true;
            return true;
        }

        return false;
    }

    [System.Serializable]
    private class TriggerCondition {
        public enum TriggerType {
            Int_Value_Threshold,
            Float_Value_Threshold,
            Float_Value_Threshold01,
            String_Equals,
            Required_Attributes,
        }
        [SerializeField] private TriggerType triggerType;

        [SerializeField] private ObservableVariable observedVariable;
        [SerializeField] private string comparedValue;

        public bool ConditionSatisfied { get; private set; }

        public void SubscribeToProperty() {
            switch (triggerType) {
                case TriggerType.Int_Value_Threshold:
                    ObservableProperty<int> targetIntProperty = observedVariable as ObservableProperty<int>;
                    targetIntProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
                        ConditionSatisfied = int.TryParse(comparedValue, out int comparedIntVal) && newVal >= comparedIntVal;
                    });
                    break;
                case TriggerType.Float_Value_Threshold:
                case TriggerType.Float_Value_Threshold01:
                    ObservableProperty<float> targetFloatProperty = observedVariable as ObservableProperty<float>;
                    targetFloatProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
                        ConditionSatisfied = float.TryParse(comparedValue, out float comparedFloatVal) && newVal >= comparedFloatVal;
                    });
                    break;
                case TriggerType.String_Equals:
                    ObservableProperty<string> targetStringProperty = observedVariable as ObservableProperty<string>;
                    targetStringProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
                        ConditionSatisfied = newVal == comparedValue;
                    });
                    break;
                case TriggerType.Required_Attributes:
                    HashSetStringProperty hashSetStringProperty = observedVariable as HashSetStringProperty;
                    hashSetStringProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
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
        private List<string> strList = new();
        private ReorderableList strReorderableList;



        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            position.height = PropertyRectHeight;
            showContents = EditorGUI.Foldout(position, showContents, label);

            if (showContents) {
                EditorGUI.indentLevel++;

                SerializedProperty triggerTypeProp = property.FindPropertyRelative("triggerType");
                SerializedProperty observedVariableProp = property.FindPropertyRelative("observedVariable");

                GUIContent comparedValueLabel = new GUIContent("Compared");
                SerializedProperty comparedValueProperty = property.FindPropertyRelative("comparedValue");

                position.y += PropertyRectHeightWithMargins;
                EditorGUI.PropertyField(position, triggerTypeProp, new GUIContent("Trigger Type"));

                position.y += PropertyRectHeightWithMargins * 1.5f;
                bool doesObservedVariableMatchTriggerType = (TriggerCondition.TriggerType)triggerTypeProp.enumValueIndex switch {
                    TriggerCondition.TriggerType.Int_Value_Threshold => observedVariableProp.objectReferenceValue is ObservableProperty<int>,
                    TriggerCondition.TriggerType.Float_Value_Threshold => observedVariableProp.objectReferenceValue is ObservableProperty<float>,
                    TriggerCondition.TriggerType.Float_Value_Threshold01 => observedVariableProp.objectReferenceValue is ObservableProperty<float>,
                    TriggerCondition.TriggerType.String_Equals => observedVariableProp.objectReferenceValue is ObservableProperty<string>,
                    TriggerCondition.TriggerType.Required_Attributes => observedVariableProp.objectReferenceValue is HashSetStringProperty,
                    _ => false,
                };
                if (!doesObservedVariableMatchTriggerType) {
                    observedVariableProp.objectReferenceValue = null;
                }
                EditorGUI.PropertyField(position, observedVariableProp, new GUIContent("Property"));

                position.y += PropertyRectHeightWithMargins;
                switch ((TriggerCondition.TriggerType)triggerTypeProp.enumValueIndex) {
                    case TriggerCondition.TriggerType.Int_Value_Threshold:
                        int.TryParse(comparedValueProperty.stringValue, out int intVal);

                        comparedValueProperty.stringValue = EditorGUI.IntField(position, comparedValueLabel, intVal).ToString();
                        break;
                    case TriggerCondition.TriggerType.Float_Value_Threshold:
                        float.TryParse(comparedValueProperty.stringValue, out float floatVal);

                        comparedValueProperty.stringValue = EditorGUI.FloatField(position, comparedValueLabel, floatVal).ToString();
                        break;
                    case TriggerCondition.TriggerType.Float_Value_Threshold01:
                        float.TryParse(comparedValueProperty.stringValue, out float float01Val);
                        float01Val = Mathf.Clamp01(float01Val);

                        comparedValueProperty.stringValue = EditorGUI.Slider(position, comparedValueLabel, float01Val, 0, 1).ToString();
                        break;
                    case TriggerCondition.TriggerType.String_Equals:
                        comparedValueProperty.stringValue = EditorGUI.TextField(position, comparedValueLabel, comparedValueProperty.stringValue);
                        break;
                    case TriggerCondition.TriggerType.Required_Attributes:
                        comparedValueProperty.stringValue = EditorGUI.TextField(position, comparedValueLabel, comparedValueProperty.stringValue);
                        break;
                }

                EditorGUI.indentLevel--;
            }


            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return showContents ? PropertyRectHeightWithMargins * 4.5f : base.GetPropertyHeight(property, label);
        }
    }
#endif
}
