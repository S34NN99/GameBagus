using System.Collections.Generic;

using UnityEngine;

public class CallEventContent : MonoBehaviour {
    [SerializeField] [RuntimeString(5)] private string _mainBody;
    public string MainBody => ObservableVariable.ConvertToRuntimeText(_mainBody);

    [SerializeField] private ProjectEventAction[] _availableActions;
    public IReadOnlyList<ProjectEventAction> AvailableActions => _availableActions;

    [SerializeField] private CandleProfile _callerProfile;
    public CandleProfile CallerProfile => _callerProfile;

    public void DisplayEvent(Phone phone) {
        PhoneCallAlert phoneCallAlert = phone.PhoneCallAlert;

        phoneCallAlert.Show();
        phoneCallAlert.Message.DisplayMessage(CallerProfile, MainBody);
        phoneCallAlert.SetActions(AvailableActions);
    }
}
