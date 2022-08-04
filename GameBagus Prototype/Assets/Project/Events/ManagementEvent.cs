
using UnityEngine;

[RequireComponent(typeof(ProjectEventTrigger))]
public class ManagementEvent : GameEventBase<GroupChat> {
    [Space]
    [SerializeField] private ProjectEventAction[] availableActions;
    [SerializeField] private ProjectEventAction defaultAction;

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
