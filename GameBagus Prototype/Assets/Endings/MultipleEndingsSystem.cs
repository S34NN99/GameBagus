using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MultipleEndingsSystem : MonoBehaviour {
    private Dictionary<string, int> _attributes;
    public Dictionary<string, int> Attributes => _attributes;

    public UnityEvent OnChanged;

    private void Awake() {
        _attributes = new();
    }

    #region Bool State
    public void AddBoolState(string attribute) {
        Attributes.Add(attribute, 1);
        OnChanged.Invoke();
    }

    public void RemoveBoolState(string attribute) {
        if (Attributes.ContainsKey(attribute)) {
            Attributes[attribute] = 0;
        } else {
            Attributes.Add(attribute, 0);
        }
        OnChanged.Invoke();
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

        CheckConditions(numState);
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

        CheckConditions(numState);
    }

    public int NumStateVal(string numState) {
        if (Attributes.TryGetValue(numState, out int numStateVal)) {
            return numStateVal;
        }
        return 0;
    }
    #endregion

    #region Conditions
    public void CheckConditions(string attribute)
    {
        switch (attribute)
        {
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

    void DissenterAndGoodLapdogCondition()
    {
        if (NumStateVal("ProWorkerActions") >= 4)
            AddBoolState("Dissenter");
        else if (NumStateVal("ProWorkerActions") <= 4)
            AddBoolState("TheGoodLapdog");
    }

    void PissedCCondition()
    {
        if(NumStateVal("C-StaffDisastisfaction") >= 6)
            AddBoolState("PissedC");
    }

    void JoCondition()
    {
        if (NumStateVal("BuddyJo") <= 0)
            AddBoolState("FoeofJo");
        else if(NumStateVal("BuddyJo") >= 2)
            AddBoolState("FriendofJo");
    }

    #endregion Conditions

    public void Save() {

    }

    public void RetrieveData() {

    }
}
