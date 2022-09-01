using System.Collections.Generic;

using UnityEngine;

public class CallEventContent : MonoBehaviour {
    [Header("Content")]
    [SerializeField] [RuntimeString] private string _mainBody;
    public string MainBody => ObservableVariable.ConvertToRuntimeText(_mainBody);

    [Header("Actions")]
    [SerializeField] private ProjectEventAction[] _availableActions;
    public IReadOnlyList<ProjectEventAction> AvailableActions => _availableActions;

    [SerializeField] private CandleProfile _bossProfile;
    public CandleProfile BossProfile => _bossProfile;

    public void TriggerEvent(GroupChat groupChat) {
        PhoneCallAlert phoneCallAlert = groupChat.PhoneCallAlert;

        phoneCallAlert.Show();
        phoneCallAlert.Message.DisplayMessage(BossProfile, MainBody);
        phoneCallAlert.SetActions(AvailableActions);
    }
}
