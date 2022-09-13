using UnityEngine;

public class FloatTrigger : MonoBehaviour {
    private enum EquationType {
        Greater_Than,
        Greater_Than_Equal,
        Equal,
        Smaller_Than,
        Smaller_Than_Equal,
    }

    [SerializeField] private EquationType equation;

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

        switch (equation) {
            case EquationType.Greater_Than:
                outputProp.Value = new_Val > comparedVal;
                break;
            case EquationType.Greater_Than_Equal:
                outputProp.Value = new_Val >= comparedVal;
                break;
            case EquationType.Equal:
                outputProp.Value = new_Val == comparedVal;
                break;
            case EquationType.Smaller_Than:
                outputProp.Value = new_Val < comparedVal;
                break;
            case EquationType.Smaller_Than_Equal:
                outputProp.Value = new_Val <= comparedVal;
                break;
        }
    }
}
