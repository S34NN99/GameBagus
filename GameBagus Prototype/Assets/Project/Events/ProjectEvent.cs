using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ProjectEvent : MonoBehaviour {
    [SerializeField] private ProjectEventTrigger trigger;

    [SerializeField] private UnityEvent onEventFired;

    [Space]
    [Tooltip("You guys can leave a comment about what the event should do")]
    [SerializeField] private string remarksForTech;

#if UNITY_EDITOR
    // auto assign trigger
    protected virtual void OnValidate() {
        if (trigger == null) {
            if (!TryGetComponent(out trigger)) {
                trigger = gameObject.AddComponent<ProjectEventTrigger>();
            }
        }
    }
#endif

    protected virtual void Update() {
        if (trigger.GetTrigger()) {
            onEventFired.Invoke();
        }
    }
}