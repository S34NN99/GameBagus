
using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

[RequireComponent(typeof(ProjectEventTrigger))]
public class ProjectEvent : GameEventBase<GroupChat> {
    [SerializeField] private float displayDuration;

    //[SerializeField] private ProjectEventType projectEventType;

    //[Space]
    //[SerializeField] private ProjectEventAction[] _availableActions;
    //public IReadOnlyList<ProjectEventAction> AvailableActions => _availableActions;

#if UNITY_EDITOR
    // auto assign trigger
    protected override void OnValidate() {
        base.OnValidate();
    }
#endif

    protected override void Update() {
        base.Update();
    }

    protected override void DisplayEvent(GroupChat groupChat) {

    }
}