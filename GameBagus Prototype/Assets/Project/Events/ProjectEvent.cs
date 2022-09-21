using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ProjectEventTrigger))]
public class ProjectEvent : MonoBehaviour {
    [SerializeField] private ProjectEventTrigger _trigger;
    public ProjectEventTrigger Trigger => _trigger;

    [SerializeField] private UnityEvent onEventFired;

    [Space]
    [Tooltip("You guys can leave a comment about what the event should do")]
    [SerializeField] private string remarksForTech;

#if UNITY_EDITOR
    private void OnValidate() {
        _trigger = GetComponent<ProjectEventTrigger>();
    }
#endif

    private void Update() {
        if (Trigger.GetTrigger()) {
            onEventFired.Invoke();
        }
    }
}
