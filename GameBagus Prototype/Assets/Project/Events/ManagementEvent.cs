
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(ProjectEventTrigger))]
public class ManagementEvent : GameEventBase<GroupChat> {
    [Header("Content")]
    [SerializeField] [RuntimeString] private string _title;
    public string Title => ObservableVariable.ConvertToRuntimeText(_title);

    [SerializeField] [RuntimeString] private string _mainBody;
    public string MainBody => ObservableVariable.ConvertToRuntimeText(_mainBody);

    [SerializeField] [RuntimeString] private string _footer;
    public string Footer => ObservableVariable.ConvertToRuntimeText(_footer);

    [SerializeField] private float _displayDuration;
    public float DisplayDuration => _displayDuration;

    [Header("Actions")]
    [Tooltip("First action is the default action")]
    [SerializeField] private ProjectEventAction[] _availableActions;
    public IReadOnlyList<ProjectEventAction> AvailableActions => _availableActions;
    public ProjectEventAction DefaultAction => AvailableActions[0];

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
        groupChat.ShowBossMessage(this);
    }
}
