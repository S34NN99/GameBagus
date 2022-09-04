using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

#endif

public class ProjectEventTrigger : MonoBehaviour {
    //[SerializeField] private TriggerCondition[] conditions_old;
    [SerializeField] private BoolProperty[] conditions;

    [SerializeField] private bool triggerOnce = true;

    [Space]
    [Tooltip("You guys can leave a comment about what the event should do")]
    [SerializeField] private string remarksForTech;

    private bool hasEventFired;

    //private void Start() {
    //    foreach (var condition in conditions) {
    //condition.SubscribeToProperty();
    //    }
    //}

    /// <summary>
    /// Allows the event's trigger to be delayed until the next time this function is called, regardless of when the value changed.
    /// </summary>
    /// <returns></returns>
    public bool GetTrigger() {
        if (hasEventFired && triggerOnce) return false;

        bool allConditionsMet = true;
        //foreach (var condition in conditions) {
        //    if (!condition.ConditionSatisfied) {
        //        allConditionsMet = false;
        //        break;
        //    }
        //}
        foreach (var condition in conditions) {
            if (!condition.Value) {
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

    //    [System.Serializable]
    //    [System.Obsolete]
    //    private class TriggerCondition {
    //        public enum TriggerType {
    //            Num_Threshold,
    //            Num_Compare,
    //            String_Compare,
    //            Required_Attributes,
    //        }
    //        [SerializeField] private TriggerType triggerType;

    //        [SerializeField] private bool isLatch;

    //        [SerializeField] private ObservableVariable[] observedVariables;
    //        [SerializeField] private string comparedValue;

    //        public bool ConditionSatisfied { get; private set; }

    //        public void SubscribeToProperty() {
    //            switch (triggerType) {
    //                case TriggerType.Num_Threshold:

    //                    var targetFloatProperty = observedVariables[0] as ObservableProperty<float>;
    //                    if (targetFloatProperty != null) {
    //                        targetFloatProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
    //                            if (isLatch && ConditionSatisfied) {
    //                                return;
    //                            }
    //                            ConditionSatisfied = float.TryParse(comparedValue, out float comparedFloatVal) && newVal >= comparedFloatVal;
    //                        });
    //                    } else {
    //                        var targetIntProperty1 = observedVariables[0] as ObservableProperty<int>;
    //                        if (targetIntProperty1 != null) {
    //                            targetIntProperty1.OnValueUpdated.AddListener((oldVal, newVal) => {
    //                                if (isLatch && ConditionSatisfied) {
    //                                    return;
    //                                }
    //                                ConditionSatisfied = int.TryParse(comparedValue, out int comparedIntVal) && newVal >= comparedIntVal;

    //                            });
    //                        }
    //                    }
    //                    break;
    //                case TriggerType.Num_Compare:
    //                    var targetFloatProperty1 = observedVariables[0] as ObservableProperty<float>;
    //                    if (targetFloatProperty1 != null) {
    //                        var targetFloatProperty2 = observedVariables[1] as ObservableProperty<float>;
    //                        targetFloatProperty1.OnValueUpdated.AddListener((oldVal, newVal) => {
    //                            if (isLatch && ConditionSatisfied) {
    //                                return;
    //                            }
    //                            ConditionSatisfied = newVal == targetFloatProperty2.Value;
    //                        });
    //                    } else {
    //                        var targetIntProperty1 = observedVariables[0] as ObservableProperty<int>;
    //                        var targetIntProperty2 = observedVariables[1] as ObservableProperty<int>;
    //                        if (targetIntProperty1 != null) {
    //                            targetIntProperty1.OnValueUpdated.AddListener((oldVal, newVal) => {
    //                                if (isLatch && ConditionSatisfied) {
    //                                    return;
    //                                }
    //                                ConditionSatisfied = newVal == targetIntProperty2.Value;
    //                            });
    //                        }
    //                    }
    //                    break;
    //                case TriggerType.String_Compare:
    //                    var targetStringProperty = observedVariables[0] as ObservableProperty<string>;
    //                    targetStringProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
    //                        if (isLatch && ConditionSatisfied) {
    //                            return;
    //                        }
    //                        ConditionSatisfied = newVal == comparedValue;
    //                    });
    //                    break;
    //                case TriggerType.Required_Attributes:
    //                    var hashSetStringProperty = observedVariables[0] as HashSetStringProperty;
    //                    hashSetStringProperty.OnValueUpdated.AddListener((oldVal, newVal) => {
    //                        if (isLatch && ConditionSatisfied) {
    //                            return;
    //                        }
    //                        ConditionSatisfied = hashSetStringProperty.GetValueAsText() == comparedValue;
    //                    });
    //                    break;
    //            }
    //        }
    //    }

    //#if UNITY_EDITOR
    //    [CustomPropertyDrawer(typeof(TriggerCondition))]
    //    private class TriggerConditionDrawer : PropertyDrawer {
    //        private const float PropertyRectHeight = 18;
    //        private const float PropertyRectHeightWithMargins = 20;

    //        private bool showContents;
    //        private bool floatPropClamp01;

    //        private int propertiesDrawn;

    //        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    //            EditorGUI.BeginProperty(position, label, property);

    //            position.height = PropertyRectHeight;
    //            showContents = EditorGUI.Foldout(position, showContents, label);

    //            if (showContents) {
    //                EditorGUI.indentLevel++;

    //                SerializedProperty triggerTypeProp = property.FindPropertyRelative("triggerType");
    //                SerializedProperty isLatchProp = property.FindPropertyRelative("isLatch");
    //                SerializedProperty observedVariableProp = property.FindPropertyRelative("observedVariables");

    //                GUIContent comparedValueLabel = new GUIContent("Compared");
    //                SerializedProperty comparedValueProperty = property.FindPropertyRelative("comparedValue");

    //                position.y += PropertyRectHeightWithMargins;
    //                EditorGUI.PropertyField(position, triggerTypeProp, new GUIContent("Trigger Type"));

    //                position.y += PropertyRectHeightWithMargins;
    //                EditorGUI.PropertyField(position, isLatchProp, new GUIContent("Is Latch", "If true, when the condition is satisfied once, it remains satisfied forever."));

    //                position.y += PropertyRectHeightWithMargins * 1.5f;
    //                if (observedVariableProp.arraySize == 0) {
    //                    observedVariableProp.arraySize = 1;
    //                }

    //                switch ((TriggerCondition.TriggerType)triggerTypeProp.enumValueIndex) {
    //                    case TriggerCondition.TriggerType.Num_Threshold:
    //                        DrawNumThresholdCondition();
    //                        break;
    //                    case TriggerCondition.TriggerType.Num_Compare:
    //                        DrawFloatCompareCondition();
    //                        break;
    //                    case TriggerCondition.TriggerType.String_Compare:
    //                        DrawStringCompareCondition();
    //                        break;
    //                    case TriggerCondition.TriggerType.Required_Attributes:
    //                        DrawRequiredAttributesCondition();
    //                        break;
    //                    default:
    //                        break;
    //                }

    //                EditorGUI.indentLevel--;

    //                void DrawNumThresholdCondition() {
    //                    SetPropertiesDrawnCount(observedVariableProp, 1);

    //                    SerializedProperty firstObservedProp = observedVariableProp.GetArrayElementAtIndex(0);
    //                    if (firstObservedProp.objectReferenceValue is ObservableProperty<float>) {
    //                        EditorGUI.PropertyField(position, observedVariableProp.GetArrayElementAtIndex(0), new GUIContent("Property 1"));

    //                        position.y += PropertyRectHeightWithMargins;
    //                        floatPropClamp01 = EditorGUI.Toggle(position, "Clamp 01", floatPropClamp01);

    //                        propertiesDrawn++;

    //                        position.y += PropertyRectHeightWithMargins;
    //                        float.TryParse(comparedValueProperty.stringValue, out float floatVal);
    //                        if (floatPropClamp01) {
    //                            comparedValueProperty.stringValue = EditorGUI.Slider(position, comparedValueLabel, Mathf.Clamp01(floatVal), 0, 1).ToString();
    //                        } else {
    //                            comparedValueProperty.stringValue = EditorGUI.FloatField(position, comparedValueLabel, floatVal).ToString();
    //                        }
    //                    } else {
    //                        CheckPropertyType<ObservableProperty<int>>(firstObservedProp);
    //                        EditorGUI.PropertyField(position, observedVariableProp.GetArrayElementAtIndex(0), new GUIContent("Property 1"));

    //                        position.y += PropertyRectHeightWithMargins;
    //                        int.TryParse(comparedValueProperty.stringValue, out int intVal);
    //                        comparedValueProperty.stringValue = EditorGUI.IntField(position, comparedValueLabel, intVal).ToString();
    //                    }

    //                }
    //                void DrawFloatCompareCondition() {
    //                    SetPropertiesDrawnCount(observedVariableProp, 2);

    //                    SerializedProperty firstObservedProp = observedVariableProp.GetArrayElementAtIndex(0);
    //                    SerializedProperty secondObservedProp = observedVariableProp.GetArrayElementAtIndex(1);
    //                    if (firstObservedProp.objectReferenceValue is ObservableProperty<float>) {
    //                        CheckPropertyType<ObservableProperty<float>>(secondObservedProp);
    //                    } else if (firstObservedProp.objectReferenceValue is ObservableProperty<int>) {
    //                        CheckPropertyType<ObservableProperty<int>>(secondObservedProp);
    //                    } else {
    //                        firstObservedProp.objectReferenceValue = null;
    //                        secondObservedProp.objectReferenceValue = null;
    //                    }

    //                    EditorGUI.PropertyField(position, firstObservedProp, new GUIContent("Property 1"));
    //                    position.y += PropertyRectHeightWithMargins;
    //                    EditorGUI.PropertyField(position, secondObservedProp, new GUIContent("Property 2"));

    //                    position.y += PropertyRectHeightWithMargins;
    //                    EditorGUI.LabelField(position, "Condition is true when both properties share the same value");
    //                }
    //                void DrawStringCompareCondition() {
    //                    SetPropertiesDrawnCount(observedVariableProp, 1);

    //                    CheckPropertyType<ObservableProperty<string>>(observedVariableProp.GetArrayElementAtIndex(0));
    //                    EditorGUI.PropertyField(position, observedVariableProp.GetArrayElementAtIndex(0), new GUIContent("Property 1"));

    //                    position.y += PropertyRectHeightWithMargins;
    //                    comparedValueProperty.stringValue = EditorGUI.TextField(position, comparedValueLabel, comparedValueProperty.stringValue);
    //                }
    //                void DrawRequiredAttributesCondition() {
    //                    SetPropertiesDrawnCount(observedVariableProp, 1);

    //                    CheckPropertyType<HashSetStringProperty>(observedVariableProp.GetArrayElementAtIndex(0));
    //                    EditorGUI.PropertyField(position, observedVariableProp.GetArrayElementAtIndex(0), new GUIContent("Property 1"));

    //                    position.y += PropertyRectHeightWithMargins;
    //                    comparedValueProperty.stringValue = EditorGUI.TextField(position, comparedValueLabel, comparedValueProperty.stringValue);

    //                }
    //            }


    //            EditorGUI.EndProperty();
    //        }

    //        private void SetPropertiesDrawnCount(SerializedProperty prop, int propertiesRequired) {
    //            if (prop.arraySize < propertiesRequired) {
    //                prop.arraySize = propertiesRequired;
    //            }

    //            propertiesDrawn = propertiesRequired;
    //        }

    //        private void CheckPropertyType<T>(SerializedProperty prop) {
    //            if (prop.objectReferenceValue is not T) {
    //                prop.objectReferenceValue = null;
    //            }
    //        }

    //        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
    //            return showContents ? PropertyRectHeightWithMargins * (propertiesDrawn + 4.5f) : base.GetPropertyHeight(property, label);
    //        }
    //    }
    //#endif
}
