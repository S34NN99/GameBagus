
using UnityEngine;

public class RandomisedFloatProperty : FloatProperty {
    [SerializeField] private FloatRandomiserBase randomiser;

    private void Awake() {
        Value = randomiser.RandomiseFloat();
    }
}