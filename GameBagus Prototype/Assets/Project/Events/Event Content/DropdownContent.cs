
using UnityEngine;
using UnityEngine.Events;

public class DropdownContent : MonoBehaviour {
    [Header("Content")]
    [SerializeField] [RuntimeString(2)] private string _title = "Urgent Issue";
    public string Title => ObservableVariable.ConvertToRuntimeText(_title);

    [SerializeField] [RuntimeString(5)] private string _message = "Too many bugs";
    public string Message => ObservableVariable.ConvertToRuntimeText(_message);

    [SerializeField] [RuntimeString(5)] private string _popUpText = "All candles -5 HP";
    public string PopUpText => ObservableVariable.ConvertToRuntimeText(_popUpText);

    [SerializeField] [RuntimeString(5)] private string _replyMessage = "Ok";
    public string ReplyMessage => ObservableVariable.ConvertToRuntimeText(_replyMessage);

    [SerializeField] private UnityEvent onDismissed;

    public void DisplayEvent(Phone phone) {
        //PhoneNotificationBanner notificationBanner = phone.CreateNotification();
        //notificationBanner.Show();
        //notificationBanner.TitleProp.Value = Title;
        //notificationBanner.MessageProp.Value = Message;
        //notificationBanner.PopUpTextProp.Value = PopUpText;
        //notificationBanner.ReplyMessageProp.Value = ReplyMessage;

        //notificationBanner.OnDismissed.AddListener(onDismissed.Invoke);
    }
}