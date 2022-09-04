
using UnityEngine;

[CreateAssetMenu(menuName = "Utility/Float Step Randomiser")]
public class FloatStepRandomiser : FloatRandomiserBase {
    [SerializeField] private float _constant;
    public float Constant => _constant;

    [SerializeField] private int _steps;
    public int Steps => _steps;

    [SerializeField] private float _stepSize;
    public float StepSize => _stepSize;

    public override float RandomiseFloat() {
        return Constant + (StepSize * Random.Range(0, Steps + 1));
    }
}
