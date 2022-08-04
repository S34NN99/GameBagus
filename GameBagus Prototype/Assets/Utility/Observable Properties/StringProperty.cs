using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class StringProperty : ObservableProperty<string> {
    public override string GetValueAsText() => Value;
}
