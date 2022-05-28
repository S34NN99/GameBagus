using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class zDebugPrinter : MonoBehaviour {
    public string printText;

    public void Print() {
        print(printText);
    }
}
