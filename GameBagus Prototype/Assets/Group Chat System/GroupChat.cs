using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GroupChat : MonoBehaviour {
    [SerializeField] private GameObject chatMessageTemplate;
    [SerializeField] private GameObject bossMessageTemplate;

    [SerializeField] private PhoneNotificationBanner notificationBanner;
    [SerializeField] private PhoneCallAlert phoneCallAlert;

    private void Awake() {

    }

    public void ShowPanel() {

    }

    public void HidePanel() {

    }

    public void SendTextMessage(string message) {

    }

    public void SendMessageFromBoss(string message) {

    }

    public void ShowNotificationAlertBanner() {

    }

    public void ShowPhoneCallAlert() {
    }
}

public class GroupChatBossMessage : GroupChatMessage {
    [SerializeField] private Image profilePic;
    [SerializeField] private TextMeshProUGUI profileNameText;
    [SerializeField] private TextMeshProUGUI messageText;

}

public class PhoneNotificationBanner : MonoBehaviour {
}

public class PhoneCallAlert : MonoBehaviour {
}