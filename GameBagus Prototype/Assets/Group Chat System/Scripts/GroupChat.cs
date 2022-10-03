using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class GroupChat : MonoBehaviour {
    [Header("Templates")]
    [SerializeField] private GameObject _chatMessageTemplate;
    public GameObject ChatMessageTemplate => _chatMessageTemplate;

    [SerializeField] private GameObject _playerChatMessageTemplate;
    public GameObject PlayerChatMessageTemplate => _playerChatMessageTemplate;

    private List<TextHeightFitter> messagesInChat = new();
    private Queue<TextHeightFitter> chatMessageQueue = new();
    private bool readyToSendNextMessage = true;
    private TextHeightFitter currentMessage;

    [Space]
    [SerializeField] private float textCooldown = 1f;
    private float textCooldownTimer;

    [SerializeField] private RectTransform _chatMessageParent;
    private RectTransform ChatMessageParent => _chatMessageParent;

    [SerializeField] private ScrollRect _scrollView;
    private ScrollRect ScrollView => _scrollView;

    private int isAutoScrolling;
    private float scrollLockPos;

    [SerializeField] private CandleManager _cm;
    public CandleManager CM => _cm;

    private void Awake() {
        if (_cm == null) {
            _cm = GameManager.Instance.CandleManager;
        }
    }

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

        if (isAutoScrolling != 0) {
            float targetHeight = scrollLockPos / ScrollView.content.rect.height;
            ScrollView.verticalScrollbar.value = 1 + targetHeight;
        }
    }

    public void StopAutoScroll() {
        isAutoScrolling++;
        if (currentMessage != null) {
            scrollLockPos = currentMessage.TargetRectTransform.anchoredPosition.y;
        }
    }

    public void ResumeAutoScroll() {
        isAutoScrolling--;
        ScrollView.verticalScrollbar.value = 0;
    }

    public T CreateMessage<T>(GameObject template) where T : MonoBehaviour {
        T chatMessage = Instantiate(template, ChatMessageParent).GetComponent<T>();
        chatMessage.gameObject.SetActive(false);

        QueueMessage(chatMessage.GetComponent<TextHeightFitter>());

        return chatMessage;
    }

    public void SendTextMessage(CandleProfile profile, string message) {
        if (message == null || message == "") {
            return;
        }

        ChatMessage chatMessage = CreateMessage<ChatMessage>(ChatMessageTemplate);
        chatMessage.DisplayMessage(profile, message);
    }

    public void InjectMessage(GameObject messageObj) {
        messageObj.transform.SetParent(ChatMessageParent);
        messageObj.gameObject.SetActive(false);

        QueueMessage(messageObj.GetComponent<TextHeightFitter>());
    }

    private void QueueMessage(TextHeightFitter message) {
        messagesInChat.Add(message);
        chatMessageQueue.Enqueue(message);

        message.SetHeightTo(40);
        message.RecalculateTextHeight();
    }
}
