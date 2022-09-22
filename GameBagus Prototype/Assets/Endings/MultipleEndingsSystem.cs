using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MultipleEndingsSystem : MonoBehaviour {
    private Dictionary<string, int> _attributes;
    public Dictionary<string, int> Attributes => _attributes;

    private void Awake() {
        _attributes = new();
    }

    #region Bool State
    public void AddBoolState(string attribute) {
        Attributes.Add(attribute, 1);
    }

    public void RemoveBoolState(string attribute) {
        if (Attributes.ContainsKey(attribute)) {
            Attributes[attribute] = 0;
        } else {
            Attributes.Add(attribute, 0);
        }
    }

    public bool HasBoolState(string attribute) {
        if (Attributes.TryGetValue(attribute, out int count)) {
            return count > 0;
        }
        return false;
    }
    #endregion

    #region Num State 
    public void IncrementNumState(string numState) {
        if (Attributes.ContainsKey(numState)) {
            Attributes[numState] += 1;
        } else {
            Attributes.Add(numState, 1);
        }
    }

    public void DecrementNumState(string numState) {
        if (Attributes.ContainsKey(numState)) {
            Attributes[numState] -= 1;
            if (Attributes[numState] < 0) {
                Attributes[numState] = 0;
            }
        } else {
            Attributes.Add(numState, 0);
        }
    }

    public int NumStateVal(string numState) {
        if (Attributes.TryGetValue(numState, out int numStateVal)) {
            return numStateVal;
        }
        return 0;
    }
    #endregion

    public void Save() {

    }

    public void RetrieveData() {

    }
}
