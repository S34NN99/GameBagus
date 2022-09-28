
using UnityEngine;

public class NumStateProperty : MonoBehaviour {
    [SerializeField] private string attribute;
    [SerializeField] private IntProperty outputProp;
    [SerializeField] private MultipleEndingsSystem mes;

    public void CheckNumState() {
        outputProp.Value = mes.NumStateVal(attribute);
    }
}