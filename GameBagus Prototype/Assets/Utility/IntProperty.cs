using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class IntProperty : ObservableProperty<int> {
    public override int Value {
        get => base.Value;
        set {
            if (_value != value) {
                OnValueUpdated.Invoke(_value, value);
                _value = value;
            }
        }
    }

    public override string GetValueAsText() => Value.ToString();
}
