using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestScript : MonoBehaviour {
    public bool doTest;

    public float addedForce;

    public Rigidbody2D targetRb;

    private void Update() {
        if (doTest) {
            TestFunc();
            doTest = false;
        }
    }

    public void TestFunc() {
        targetRb.AddForceAtPosition(new Vector2(addedForce, 0), targetRb.position + new Vector2(0, 1));
    }
}
