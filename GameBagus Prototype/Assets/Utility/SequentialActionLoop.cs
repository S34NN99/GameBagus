using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class SequentialActionLoop : MonoBehaviour {
    [SerializeField] private bool resetAndFireOnEnabled = true;

    [Space]
    [SerializeField] private UnityEvent[] actionLoop;

    private int _counter;
    private int Counter { get => _counter; set => _counter = value; }

    private void OnEnable() {
        if (resetAndFireOnEnabled) {
            Counter = 0;
            FireNextAction();
        }
    }

    public void FireNextAction() {
        if (Counter < actionLoop.Length) {
            actionLoop[Counter].Invoke();
            Counter++;
        }

        CheckIfCounterNeedsToReset();
    }

    public void JumpBack() {
        Counter--;

        CheckIfCounterNeedsToReset();
    }

    public void SkipForward() {
        Counter++;

        CheckIfCounterNeedsToReset();
    }

    public void JumpTo(int pointerPos) {
        Counter = pointerPos;

        CheckIfCounterNeedsToReset();
    }

    private void CheckIfCounterNeedsToReset() {
        if (Counter >= actionLoop.Length || Counter < 0) {
            Counter = 0;
        }
    }
}
