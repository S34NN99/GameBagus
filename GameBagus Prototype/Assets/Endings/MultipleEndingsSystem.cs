using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MultipleEndingsSystem : MonoBehaviour {
    [SerializeField] private HashSetStringProperty _boolStates;
    public HashSetStringProperty BoolStates => _boolStates;

    private Dictionary<string, int> _numStates;
    public Dictionary<string, int> NumStates => _numStates;

    [SerializeField] private MetricsPanel metricsPanel;

    [SerializeField] private StoryCheckpoint[] _endings;
    public StoryCheckpoint[] Endings => _endings;

    private void Awake() {
        BoolStates.Value = new HashSet<string>();
    }

    #region Bool State 
    public void AddBoolState(string boolState) {
        BoolStates.Add(boolState);
    }

    public void RemoveBoolState(string boolState) {
        BoolStates.Remove(boolState);
    }

    public bool HasBoolState(string boolState) {
        return BoolStates.Value.Contains(boolState);
    }
    #endregion

    #region Num State 
    public void IncrementNumState(string numState) {
        if (NumStates.ContainsKey(numState)) {
            NumStates[numState] += 1;
        } else {
            NumStates.Add(numState, 1);
        }
    }

    public void DecrementNumState(string numState) {
        if (NumStates.ContainsKey(numState)) {
            NumStates[numState] -= 1;
            if (NumStates[numState] < 0) {
                NumStates[numState] = 0;
            }
        } else {
            NumStates.Add(numState, 0);
        }
    }

    public int NumStateVal(string numState) {
        if (NumStates.TryGetValue(numState, out int numStateVal)) {
            return numStateVal;
        }
        return 0;
    }
    #endregion


    //#if UNITY_EDITOR
    //    [CustomEditor(typeof(MultipleEndingsSystem))]
    //    private class MultipleEndingsSystemEditor : Editor {
    //        private MultipleEndingsSystem multipleEndingsSystem;

    //        private bool showAttributeListDropdown;

    //        private void OnEnable() {
    //            multipleEndingsSystem = target as MultipleEndingsSystem;
    //        }

    //        public override void OnInspectorGUI() {
    //            base.OnInspectorGUI();

    //            if (multipleEndingsSystem.BoolStates.Value == null) return;
    //            showAttributeListDropdown = EditorGUILayout.Foldout(showAttributeListDropdown, "Attribute List");
    //            if (showAttributeListDropdown) {
    //                EditorGUI.BeginDisabledGroup(true);
    //                EditorGUI.indentLevel++;

    //                int counter = 0;
    //                foreach (var attribute in multipleEndingsSystem.BoolStates.Value) {
    //                    EditorGUILayout.TextField("Element " + counter, attribute);
    //                    counter++;
    //                }

    //                EditorGUI.indentLevel--;
    //                EditorGUI.EndDisabledGroup();
    //            }

    //        }
    //    }
    //#endif
}
