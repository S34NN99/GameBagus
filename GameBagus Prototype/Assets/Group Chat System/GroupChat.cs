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

    private List<TextHeightFitter> messagesInChat = new();
    private Queue<TextHeightFitter> chatMessageQueue = new();
    private TextHeightFitter currentMessage;

    [SerializeField] private float textCooldown = 1f;
    private float textCooldownTimer;

    [Space]
    [SerializeField] private RectTransform _chatMessageParent;
    private RectTransform ChatMessageParent => _chatMessageParent;
    private void Update() {
        if (textCooldownTimer > 0) {
            textCooldownTimer -= Time.deltaTime;
            if (textCooldownTimer < 0) {
                textCooldownTimer = 0;
            }

            return;
        }

        if (currentMessage == null) {
            if (chatMessageQueue.Any()) {
                currentMessage = chatMessageQueue.Dequeue();
                currentMessage.gameObject.SetActive(true);
            }
        } else {
            if (!currentMessage.IsAnimating) {
                // stops animating, free to display new message on next frame
                currentMessage = null;
                textCooldownTimer = textCooldown;
            }
        }
    }

    public void ShowPanel() {

    }

    public void HidePanel() {

    }

    public T CreateMessage<T>(GameObject template) where T : MonoBehaviour {
        T chatMessage = Instantiate(template, ChatMessageParent).GetComponent<T>();
        chatMessage.gameObject.SetActive(false);

        QueueMessage(chatMessage.GetComponent<TextHeightFitter>());

        return chatMessage;
    }

    public CandleMessage CreateTextMessage() => CreateMessage<CandleMessage>(ChatMessageTemplate);
    public CandleMessage CreatePlayerMessage() => CreateMessage<CandleMessage>(PlayerChatMessageTemplate);
    public GroupChatBossMessage CreateBossMessage() => CreateMessage<GroupChatBossMessage>(BossMessageTemplate);

    public void SendTextMessage(CandleProfile profile, string message) {
        if (message == null || message == "") {
            return;
        }

        CandleMessage chatMessage = CreateTextMessage();
        chatMessage.DisplayMessage(profile, message);
    }

    private void QueueMessage(TextHeightFitter message) {
        messagesInChat.Add(message);
        chatMessageQueue.Enqueue(message);

        message.SetHeightTo(40);
        message.RecalculateTextHeight();
    }
}
