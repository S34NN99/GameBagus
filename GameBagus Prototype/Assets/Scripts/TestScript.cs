using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestScript : MonoBehaviour {
    public bool doTest;

    public MultipleEndingsSystem mes;

    public string attribute1;
    public string attribute2;

    private void Update() {
        if (doTest) {
            TestFunc();
            doTest = false;
        }
    }

    public void TestFunc() {
        mes.AdjustNumState(attribute1 + ",100");
        mes.AdjustNumState(attribute2 + ",100");

        mes.ShowEnding();
    }
}
