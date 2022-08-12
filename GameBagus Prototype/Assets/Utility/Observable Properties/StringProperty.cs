using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class StringProperty : ObservableEquatableProperty<string> {
    public override string GetValueAsText() => Value;
}