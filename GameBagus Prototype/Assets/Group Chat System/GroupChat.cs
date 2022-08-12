using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;

public class GroupChat : MonoBehaviour {
    [Header("Templates")]
    [SerializeField] private GameObject chatMessageTemplate;
    [SerializeField] private GameObject bossMessageTemplate;

    [Space]
    [SerializeField] private RectTransform chatMessageParent;
    [SerializeField] private float chatMessageSpacing = 25f;
    private List<GroupChatMessage> messagesInChat = new();
    private Queue<GroupChatMessage> chatMessageQueue = new();
    private GroupChatMessage currentMessage;

    [Space]
    [SerializeField] private CandleProfile bossProfile;
    [SerializeField] private PhoneNotificationBanner notificationBanner;
    [SerializeField] private PhoneCallAlert phoneCallAlert;

    private void Awake() {

    }

    private void Update() {
        //if (currentMessage == null) {
        //    if (chatMessageQueue.Any()) {
        //        currentMessage = chatMessageQueue.Dequeue();
        //        currentMessage.gameObject.SetActive(true);

        //        ResizeGroupChat();
        //    }
        //} else if (!currentMessage.IsAnimating) {
        //    // stops animating, free to display new message on next frame
        //    currentMessage = null;
        //}
    }

    public void ShowPanel() {

    }

    public void HidePanel() {

    }

    public void SendTextMessage(CandleProfile profile, string message) {
        GroupChatMessage chatMessage = Instantiate(chatMessageTemplate, chatMessageParent).GetComponent<GroupChatMessage>();
        chatMessage.DisplayMessage(profile, message);
        messagesInChat.Add(chatMessage);
        ResizeGroupChat();

        //chatMessage.gameObject.SetActive(false);
        //chatMessageQueue.Enqueue(chatMessage);
    }

    public void ShowBossMessage(ManagementEvent managementEvent) {
        GroupChatBossMessage bossMessage = Instantiate(bossMessageTemplate, chatMessageParent).GetComponent<GroupChatBossMessage>();
        bossMessage.SetIssue(new GroupChatBossMessage.Issue {
            Title = managementEvent.Title,
            Footer = managementEvent.Footer,
            Duration = managementEvent.DisplayDuration,
        });
        bossMessage.DisplayMessage(bossProfile, managementEvent.MainBody);
        messagesInChat.Add(bossMessage);
        ResizeGroupChat();

        //bossMessage.gameObject.SetActive(false);
        //chatMessageQueue.Enqueue(bossMessage);
    }

    private void ResizeGroupChat() {
        float totalHeight = 0;
        foreach (var message in messagesInChat) {
            totalHeight += message.RT.sizeDelta.y;
        }
        totalHeight += chatMessageSpacing * messagesInChat.Count - 1;

        chatMessageParent.sizeDelta = new Vector2(chatMessageParent.sizeDelta.x, totalHeight);
    }

    public void ShowNotificationAlertBanner(ProjectEvent projectEvent) {
        notificationBanner.DisplayNotification(projectEvent.Title, projectEvent.MainBody, projectEvent.DisplayDuration);
    }

    public void ShowPhoneCallAlert(StoryEvent storyEvent) {
    }
}

public class PhoneCallAlert : MonoBehaviour {
}