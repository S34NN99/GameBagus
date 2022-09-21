using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FloatProperty : ObservableEquatableProperty<float> {
    public void IncrementBy(float increment) {
        Value += increment;
    }

    public void MultiplyBy(float multiplier) {
        Value *= multiplier;
    }
}
