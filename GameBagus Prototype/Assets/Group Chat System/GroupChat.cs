using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class GroupChat : MonoBehaviour {
    [Header("Templates")]
    [SerializeField] private GameObject _chatMessageTemplate;
    private GameObject ChatMessageTemplate => _chatMessageTemplate;

    [SerializeField] private GameObject _bossMessageTemplate;
    private GameObject BossMessageTemplate => _bossMessageTemplate;

    [SerializeField] private GameObject _playerChatMessageTemplate;
    private GameObject PlayerChatMessageTemplate => _playerChatMessageTemplate;


    [Space]
    [SerializeField] private RectTransform _chatMessageParent;
    private RectTransform ChatMessageParent => _chatMessageParent;

    [Space]
    [SerializeField] private PhoneNotificationBanner _notificationBanner;
    public PhoneNotificationBanner NotificationBanner => _notificationBanner;

    [SerializeField] private PhoneCallAlert _phoneCallAlert;
    public PhoneCallAlert PhoneCallAlert => _phoneCallAlert;

    private List<TextHeightFitter> messagesInChat = new();
    private Queue<TextHeightFitter> chatMessageQueue = new();
    private TextHeightFitter currentMessage;


    private void Awake() {

    }

    private void Update() {
        if (currentMessage == null) {
            if (chatMessageQueue.Any()) {
                currentMessage = chatMessageQueue.Dequeue();
                currentMessage.gameObject.SetActive(true);
            }
        } else {
            if (!currentMessage.IsAnimating) {
                // stops animating, free to display new message on next frame
                currentMessage = null;
            }
        }
    }

    public void ShowPanel() {

    }

    public void HidePanel() {

    }

    public CandleMessage CreateTextMessage() {
        CandleMessage chatMessage = Instantiate(ChatMessageTemplate, ChatMessageParent).GetComponent<CandleMessage>();
        chatMessage.gameObject.SetActive(false);

        QueueMessage(chatMessage.GetComponent<TextHeightFitter>());

        return chatMessage;
    }

    public void SendTextMessage(CandleProfile profile, string message) {
        CandleMessage chatMessage = CreateTextMessage();
        chatMessage.DisplayMessage(profile, message);
    }

    public GroupChatBossMessage CreateBossMessage() {
        GroupChatBossMessage bossMessage = Instantiate(BossMessageTemplate, ChatMessageParent).GetComponent<GroupChatBossMessage>();
        bossMessage.gameObject.SetActive(false);

        QueueMessage(bossMessage.GetComponent<TextHeightFitter>());

        return bossMessage;
    }

    private void QueueMessage(TextHeightFitter message) {
        messagesInChat.Add(message);
        chatMessageQueue.Enqueue(message);

        message.SetHeightTo(40);
        message.RecalculateTextHeight();
    }
}
