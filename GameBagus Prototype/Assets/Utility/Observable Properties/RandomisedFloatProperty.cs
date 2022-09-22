
using UnityEngine;

public class RandomisedFloatProperty : FloatProperty {
    [SerializeField] private FloatRandomiser _randomiser;
    public FloatRandomiser Randomiser => _randomiser;

    [SerializeField] private bool randomiseOnStart = true;

    private void Start() {
        if (randomiseOnStart) {
            Value = Randomiser.Next();
        }
    }
}