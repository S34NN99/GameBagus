
using UnityEngine;

public class Phone : MonoBehaviour {
    [SerializeField] private GroupChat _groupChat;
    public GroupChat GroupChat => _groupChat;

    [Space]
    [SerializeField] private PhoneNotificationBanner _notificationBanner;
    public PhoneNotificationBanner NotificationBanner => _notificationBanner;

    //[SerializeField] private PhoneNotificationBanner _notificationTemplate;
    //public PhoneNotificationBanner NotificationTemplate => _notificationTemplate;

    [SerializeField] private PhoneCallAlert _phoneCallAlert;
    public PhoneCallAlert PhoneCallAlert => _phoneCallAlert;


    private void Awake() {

    }



    public PhoneNotificationBanner CreateNotification() {
        return NotificationBanner;
    }

    public void DismissNotification() {

    }
}
