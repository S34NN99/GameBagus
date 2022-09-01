using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class SequentialActionLoop : MonoBehaviour {
    [SerializeField] private bool fireFirstActionOnStart;

    [Space]
    [SerializeField] private UnityEvent[] actionLoop;
    private int counter;

    private void Start() {
        if (fireFirstActionOnStart) {
            FireNextAction();
        }
    }

    public void FireNextAction() {
        if (counter < actionLoop.Length) {
            actionLoop[counter].Invoke();
            counter++;
        }

        CheckIfCounterNeedsToReset();
    }

    public void JumpBack() {
        counter--;

        CheckIfCounterNeedsToReset();
    }

    public void SkipForward() {
        counter++;

        CheckIfCounterNeedsToReset();
    }

    private void CheckIfCounterNeedsToReset() {
        if (counter >= actionLoop.Length || counter < 0) {
            counter = 0;
        }
    }
}
