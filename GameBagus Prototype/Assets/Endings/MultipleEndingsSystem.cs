using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MultipleEndingsSystem : MonoBehaviour {
    [SerializeField] private string _currentRunTitle = "Current";
    public string CurrentRunTitle { get => _currentRunTitle; set => _currentRunTitle = value; }

    [SerializeField] private GameStateDatabase _attributeDb;
    public GameStateDatabase AttributeDb => _attributeDb;

    private Dictionary<string, int> _attributes;
    public Dictionary<string, int> Attributes => _attributes;

    public UnityEvent OnChanged;

    private void Awake() {
        _attributes = new();

        RetrieveData();

        OnChanged.Invoke();
    }

    #region Bool State
    public void AddBoolState(string boolState) => SetState(boolState, 1);
    public void RemoveBoolState(string boolState) => SetState(boolState, 0);
    public bool HasBoolState(string boolState) => GetState(boolState) > 0;
    #endregion

    #region Num State 
    public void IncrementNumState(string numState) => AdjustState(numState, 1);
    public void DecrementNumState(string numState) => AdjustState(numState, -1);
    public int NumStateVal(string numState) => GetState(numState);

    /// <summary>
    /// Comma separated values. Before the comma is the name of the numstate, after the comma represents the increment.
    /// </summary>
    /// <param name="numState"></param>
    public void AdjustNumState(string numState) {
        string[] values = numState.Split(',');

        string attributeName = values[0];
        int amount = int.Parse(values[1]);

        AdjustState(attributeName, amount);
    }
    #endregion

    #region All States
    public void AdjustState(string state, int value) => SetState(state, GetState(state) + value);

    public int GetState(string state) {
        if (!Attributes.TryGetValue(state, out int stateVal)) {
            Attributes.Add(state, stateVal);
        }
        return stateVal;
    }

    public void SetState(string state, int value) {
        if (GetState(state) == value) return;

        if (Attributes.ContainsKey(state)) {
            Attributes[state] = value;
        } else {
            Attributes.Add(state, value);
        }

        //CheckConditions(state);
        CheckNumToBoolStateTriggers();
        OnChanged.Invoke();
    }
    #endregion


    #region Conditions
    public void CheckConditions(string attribute) {
        switch (attribute) {
            case "ProWorkerActions":
                DissenterAndGoodLapdogCondition();
                break;

            case "C-StaffDisastisfaction":
                PissedCCondition();
                break;

            case "BuddyJo":
                JoCondition();
                break;

            default:
                break;
        }

        OnChanged.Invoke();
    }

    void DissenterAndGoodLapdogCondition() {
        if (NumStateVal("ProWorkerActions") >= 4)
            AddBoolState("Dissenter");
        else if (NumStateVal("ProWorkerActions") <= 4)
            AddBoolState("TheGoodLapdog");
    }

    void PissedCCondition() {
        if (NumStateVal("C-StaffDisastisfaction") >= 6)
            AddBoolState("PissedC");
    }

    void JoCondition() {
        if (NumStateVal("BuddyJo") <= 0)
            AddBoolState("FoeofJo");
        else if (NumStateVal("BuddyJo") >= 2)
            AddBoolState("FriendofJo");
    }

    #endregion Conditions

    public void Save() {
        // The bool states
        foreach (var boolState in AttributeDb.BoolStateNames) {
            bool boolStateVal = HasBoolState(boolState);
            PlayerPrefs.SetInt($"{CurrentRunTitle}_{boolState}", boolStateVal ? 1 : 0);
        }

        // The num states
        foreach (var numState in AttributeDb.NumStateNames) {
            int numStateVal = NumStateVal(numState);
            PlayerPrefs.SetInt($"{CurrentRunTitle}_{numState}", numStateVal);
        }
    }

    public void RetrieveData() {
        // The bool states
        foreach (var boolState in AttributeDb.BoolStateNames) {
            int boolStateVal = PlayerPrefs.GetInt($"{CurrentRunTitle}_{boolState}", 0);
            if (boolStateVal == 1) {
                AddBoolState(boolState);
            } else {
                RemoveBoolState(boolState);
            }
        }

        // The num states
        foreach (var numState in AttributeDb.NumStateNames) {
            int numStateVal = PlayerPrefs.GetInt($"{CurrentRunTitle}_{numState}", 0);
            AdjustNumState($"{numState},{numStateVal}");
        }

        CheckNumToBoolStateTriggers();
    }

    private void CheckNumToBoolStateTriggers() {
        foreach (var trigger in AttributeDb.NumToBoolStateTriggers) {
            int numStateVal = NumStateVal(trigger.NumStateName);

            if (MyMaths.CompareIntegers(trigger.Equation, numStateVal, trigger.TargetNumVal)) {
                AddBoolState(trigger.BoolStateName);
            } else {
                RemoveBoolState(trigger.BoolStateName);
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MultipleEndingsSystem))]
    private class MES_Editor : Editor {

        private MultipleEndingsSystem mes;

        private void OnEnable() {
            mes = target as MultipleEndingsSystem;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (Application.isPlaying) {
                EditorGUILayout.LabelField("Bool and Num States", EditorStyles.boldLabel);
                EditorGUI.BeginDisabledGroup(true);
                // The num states
                foreach (var numState in mes.Attributes) {
                    EditorGUILayout.IntField(numState.Key, numState.Value);
                }
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.Space(20);
                if (GUILayout.Button("Save states NOW")) {
                    mes.Save();
                }
                if (GUILayout.Button("Clear Game States")) {
                    foreach (var boolState in mes.AttributeDb.BoolStateNames) {
                        PlayerPrefs.DeleteKey($"{mes.CurrentRunTitle}_{boolState}");
                    }

                    // The num states
                    foreach (var numState in mes.AttributeDb.NumStateNames) {
                        PlayerPrefs.DeleteKey($"{mes.CurrentRunTitle}_{numState}");
                    }
                }
            }
        }
    }
#endif
}
