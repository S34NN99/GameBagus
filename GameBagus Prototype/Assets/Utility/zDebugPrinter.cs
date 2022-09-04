using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

public class zDebugPrinter : MonoBehaviour {
    public string printText;

    public void Print() {
        print(printText);
    }

    public void Print(string text) {
        print(text);
    }

    public void PrintAsRuntimeStr() {
        print(ObservableVariable.ConvertToRuntimeText(printText));
    }

    public void PrintAsRuntimeStr(string text) {
        print(ObservableVariable.ConvertToRuntimeText(text));
    }

    public void PrintFloatChanged(float oldVal, float newVal) {
        print($"Old Val : {oldVal}, New Val : {newVal}");
    }
}
