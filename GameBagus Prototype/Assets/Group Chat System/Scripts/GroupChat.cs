using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;

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
    private bool readyToSendNextMessage = true;
    private TextHeightFitter currentMessage;

    [SerializeField] private float textCooldown = 1f;
    private float textCooldownTimer;

    [Space]
    [SerializeField] private RectTransform _chatMessageParent;
    private RectTransform ChatMessageParent => _chatMessageParent;

    [SerializeField] private ScrollRect _scrollView;
    private ScrollRect ScrollView => _scrollView;

    private bool isAutoScrolling = true;
    private float scrollLockPos;

    private void Update() {
        if (textCooldownTimer > 0) {
            textCooldownTimer -= Time.deltaTime;
            if (textCooldownTimer < 0) {
                textCooldownTimer = 0;
            }

            return;
        }

        if (readyToSendNextMessage) {
            if (chatMessageQueue.Any()) {
                currentMessage = chatMessageQueue.Dequeue();
                currentMessage.gameObject.SetActive(true);
                readyToSendNextMessage = false;
            }
        } else {
            if (!currentMessage.IsAnimating) {
                // stops animating, free to display new message on next frame
                readyToSendNextMessage = true;
                textCooldownTimer = textCooldown;
            }
        }

        if (!isAutoScrolling) {
            float targetHeight = scrollLockPos / ScrollView.content.rect.height;
            ScrollView.verticalScrollbar.value = 1 + targetHeight;
        }
    }

    public void StopAutoScroll() {
        isAutoScrolling = false;
        if (currentMessage != null) {
            scrollLockPos = currentMessage.TargetRectTransform.anchoredPosition.y;
        }
    }

    public void ResumeAutoScroll() {
        isAutoScrolling = true;
        ScrollView.verticalScrollbar.value = 0;
    }

    public T CreateMessage<T>(GameObject template) where T : MonoBehaviour {
        T chatMessage = Instantiate(template, ChatMessageParent).GetComponent<T>();
        chatMessage.gameObject.SetActive(false);

        QueueMessage(chatMessage.GetComponent<TextHeightFitter>());

        return chatMessage;
    }

    public ChatMessage CreateTextMessage() => CreateMessage<ChatMessage>(ChatMessageTemplate);
    public ChatMessage CreatePlayerMessage() => CreateMessage<ChatMessage>(PlayerChatMessageTemplate);
    public GroupChatBossMessage CreateBossMessage() => CreateMessage<GroupChatBossMessage>(BossMessageTemplate);

    public void SendTextMessage(CandleProfile profile, string message) {
        if (message == null || message == "") {
            return;
        }

        ChatMessage chatMessage = CreateTextMessage();
        chatMessage.DisplayMessage(profile, message);
    }

    private void QueueMessage(TextHeightFitter message) {
        messagesInChat.Add(message);
        chatMessageQueue.Enqueue(message);

        message.SetHeightTo(40);
        message.RecalculateTextHeight();
    }
}
