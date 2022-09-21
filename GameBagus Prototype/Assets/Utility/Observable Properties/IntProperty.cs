using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class IntProperty : ObservableEquatableProperty<int> {
    public void IncrementBy(int increment) {
        Value += increment;
    }

    public void MultiplyBy(int multiplier) {
        Value *= multiplier;
    }
}
