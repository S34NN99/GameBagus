using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Obsolete]
public class PhoneCallAlert : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI[] contentToUpdate;

    public void RefreshPopUpContent() {
        foreach (var runtimeString in contentToUpdate) {
            runtimeString.text = ObservableVariable.ConvertToRuntimeText(runtimeString.text);
        }
    }

    public void OnEnable()
    {
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnCallEvent);
    }

    public void OnDisable()
    {
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnCallEventEnded);
    }
}