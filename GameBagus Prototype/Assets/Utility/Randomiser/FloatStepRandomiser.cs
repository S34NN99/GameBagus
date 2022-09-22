
using UnityEngine;

[CreateAssetMenu(menuName = "Utility/Float Step Randomiser")]
public class FloatStepRandomiser : FloatRandomiser {
    [SerializeField] private float _constant;
    public float Constant => _constant;

    [SerializeField] private int _steps;
    public int Steps => _steps;

    [SerializeField] private float _stepSize;
    public float StepSize => _stepSize;

    private void OnValidate() {
        if (Steps < 0) {
            _steps = 0;
        }
    }

    public override float Next() {
        return Constant + (StepSize * Random.Range(0, Steps + 1));
    }
}
