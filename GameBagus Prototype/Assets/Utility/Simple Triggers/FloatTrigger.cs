using UnityEngine;

public class FloatTrigger : MonoBehaviour {
    [SerializeField] private MyMaths.ComparisonEquation equation;

    [Space]
    [Tooltip("Uses the 'constantComparedVal' value instead of 'targetVar.Value'")]
    [SerializeField] private bool useConstantVal;
    [SerializeField] private float constantComparedVal;
    [SerializeField] private FloatProperty targetVar;

    [Space]
    [SerializeField] private BoolProperty outputProp;
    [SerializeField] private bool isLatch;

    public void OnChanged(float old_val, float new_Val) {
        if (isLatch) {
            if (outputProp.Value) {
                return;
            }
        }

        float comparedVal = useConstantVal ? constantComparedVal : targetVar.Value;

        outputProp.Value = MyMaths.CompareFloats(equation, new_Val, comparedVal);
    }
}
