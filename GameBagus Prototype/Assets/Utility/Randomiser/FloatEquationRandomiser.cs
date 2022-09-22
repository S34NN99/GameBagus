using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Utility/Float Equation Randomiser")]
public class FloatEquationRandomiser : FloatRandomiser {
    [SerializeField] private float _constant;
    public float Constant => _constant;

    [SerializeField] private float _gradient;
    public float Gradient => _gradient;

    [SerializeField] private DegreeOfX[] degreesOfX;

    [SerializeField] private float _minX;
    public float MinX => _minX;

    [SerializeField] private float _maxX;
    public float MaxX => _maxX;


    public override float Next() {
        float x = Random.Range(MinX, MaxX);
        float final = Constant;
        foreach (var degreeOfX in degreesOfX) {
            final += Mathf.Pow(x, degreeOfX.Power) * degreeOfX.Multiplier;
        }
        return Constant + (Random.Range(MinX, MaxX) * Gradient);
    }

    private class DegreeOfX {
        [SerializeField] private float _multiplier;
        public float Multiplier => _multiplier;

        [SerializeField] private float _power;
        public float Power => _power;
    }
}
