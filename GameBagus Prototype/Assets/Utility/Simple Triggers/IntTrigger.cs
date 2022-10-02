using UnityEngine;

public class IntTrigger : MonoBehaviour {
    [SerializeField] private MyMaths.ComparisonEquation equation;

    [Space]
    [Tooltip("Uses the 'constantComparedVal' value instead of 'targetVar.Value'")]
    [SerializeField] private bool useConstantVal;
    [SerializeField] private int constantComparedVal;
    [SerializeField] private IntProperty targetVar;


    [Space]
    [SerializeField] private BoolProperty outputProp;
    [SerializeField] private bool isLatch;

    public void OnChanged(int old_val, int new_Val) {
        if (isLatch) {
            if (outputProp.Value) {
                return;
            }
        }

        int comparedVal = useConstantVal ? constantComparedVal : targetVar.Value;

        outputProp.Value = MyMaths.CompareIntegers(equation, new_Val, comparedVal);
    }
}
