using UnityEngine;

#if UNITY_EDITOR
#endif

public class IntTrigger : MonoBehaviour {
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
