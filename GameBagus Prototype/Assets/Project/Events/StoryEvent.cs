
using UnityEngine;

[RequireComponent(typeof(ProjectEventTrigger))]
public class StoryEvent : GameEventBase<GroupChat> {
    [Space]
    [SerializeField] private ProjectEventAction[] availableActions;

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