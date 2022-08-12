
using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

[RequireComponent(typeof(ProjectEventTrigger))]
public class ProjectEvent : GameEventBase<GroupChat> {
    [Header("Content")]
    [SerializeField] [RuntimeString] protected string _title = "Urgent Issue";
    public string Title => ObservableVariable.ConvertToRuntimeText(_title);

    [SerializeField] [RuntimeString] protected string _mainBody = "Too many bugs";
    public string MainBody => ObservableVariable.ConvertToRuntimeText(_mainBody);

    [SerializeField] private float _displayDuration = 4f;
    public float DisplayDuration => _displayDuration;

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
        groupChat.ShowNotificationAlertBanner(this);
    }
}