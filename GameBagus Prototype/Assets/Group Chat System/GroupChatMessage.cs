
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GroupChatMessage : MonoBehaviour {
    [SerializeField] private Image profilePic;
    [SerializeField] private TextMeshProUGUI profileNameText;
    [SerializeField] private TextMeshProUGUI messageText;

    public float TextHeight { get; private set; }

    public void DisplayMessage(ChatProfile profile, string message) {
        profilePic.sprite = profile.ProfilePic;
        profileNameText.text = profile.ProfileName;
        messageText.text = message;

        ReadjustTextHeight();
    }

    private void ReadjustTextHeight() {
        // todo
    }
}
