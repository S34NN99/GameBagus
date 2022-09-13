using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ProjectEventTrigger))]
public class ProjectEvent : MonoBehaviour {
    [SerializeField] private ProjectEventTrigger trigger;

    [SerializeField] private UnityEvent onEventFired;

    [Space]
    [Tooltip("You guys can leave a comment about what the event should do")]
    [SerializeField] private string remarksForTech;

    private void Update() {
        if (trigger.GetTrigger()) {
            onEventFired.Invoke();
        }
    }
}
