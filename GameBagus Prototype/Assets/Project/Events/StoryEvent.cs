using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(ProjectEventTrigger))]
public class StoryEvent : GameEventBase<GroupChat> {
    [Header("Content")]
    [SerializeField] [RuntimeString] private string _mainBody;
    public string MainBody => ObservableVariable.ConvertToRuntimeText(_mainBody);

    [Header("Actions")]
    [SerializeField] private ProjectEventAction[] _availableActions;
    public IReadOnlyList<ProjectEventAction> AvailableActions => _availableActions;

    [SerializeField] private CandleProfile _bossProfile;
    public CandleProfile BossProfile => _bossProfile;

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
        PhoneCallAlert phoneCallAlert = groupChat.PhoneCallAlert;

        phoneCallAlert.Show();
        phoneCallAlert.Message.DisplayMessage(BossProfile, MainBody);
        phoneCallAlert.SetActions(AvailableActions);
    }
}