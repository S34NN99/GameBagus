
using UnityEngine;

public class DropdownContent : MonoBehaviour {
    [Header("Content")]
    [SerializeField] [RuntimeString] private string _title = "Urgent Issue";
    public string Title => ObservableVariable.ConvertToRuntimeText(_title);

    [SerializeField] [RuntimeString] private string _mainBody = "Too many bugs";
    public string MainBody => ObservableVariable.ConvertToRuntimeText(_mainBody);

    [SerializeField] private float _displayDuration = 4f;
    public float DisplayDuration => _displayDuration;

    public void DisplayEvent(GroupChat groupChat) {
        PhoneNotificationBanner notificationBanner = groupChat.NotificationBanner;
        notificationBanner.DisplayNotification(Title, MainBody, DisplayDuration);
    }
}