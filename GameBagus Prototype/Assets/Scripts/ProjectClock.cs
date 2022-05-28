using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ProjectClock : MonoBehaviour {
    [SerializeField] private int ticksRemaining;

    [SerializeField] private UnityEvent onTimesUp;

    public void ResetClock(int newDeadline) {
        ticksRemaining = newDeadline;
    }

    public void Tick() {
        ticksRemaining--;
        if (ticksRemaining == 0) {
            onTimesUp.Invoke();
        }
    }
}
