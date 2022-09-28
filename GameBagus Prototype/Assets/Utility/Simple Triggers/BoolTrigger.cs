using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class BoolTrigger : MonoBehaviour {
    [SerializeField] private string attribute;
    [SerializeField] private BoolProperty outputProp;
    [SerializeField] private MultipleEndingsSystem mes;


    public void CheckBool() {
        outputProp.Value = mes.HasBoolState(attribute);
    }
}
