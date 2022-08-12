
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GroupChatMessage : MonoBehaviour {
    [Header("UI Components")]
    [SerializeField] protected Image profilePic;
    [SerializeField] protected TextMeshProUGUI profileNameText;
    [SerializeField] protected TextMeshProUGUI messageText;

    [Header("Height Settings")]
    [SerializeField] protected float profileNameTextHeight = 40f;
    [SerializeField] protected float minimumTextHeight = 60f;

    [Header("Animation Settings")]
    [SerializeField] protected float smoothSpeed = 0.5f;
    protected float smoothSpeedVelocity;
    [SerializeField] protected float smoothAccuracy = 0.05f;
    public bool IsAnimating { get; protected set; }

    public RectTransform RT { get; protected set; }

    public float TextHeight { get; private set; }
    public float TotalHeight { get; private set; }

    protected virtual void Awake() {
        RT = GetComponent<RectTransform>();

        //RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, 0);
    }

    protected virtual void Update() {
        AdjustHeight();
    }

    public virtual void DisplayMessage(CandleProfile profile, string message) {
        profilePic.sprite = profile.ProfilePic;
        profileNameText.text = profile.ProfileName;
        messageText.text = message;

        ReadjustTextHeight();
    }

    protected virtual void ReadjustTextHeight() {
        TextHeight = Mathf.Max(minimumTextHeight, messageText.preferredHeight);
        TotalHeight = TextHeight + profileNameTextHeight;
    }

    protected virtual void AdjustHeight() {
        Vector2 size = RT.sizeDelta;

        //if (Mathf.Abs(size.y - TotalHeight) < smoothAccuracy) {
        size.y = TotalHeight;

        //    IsAnimating = false;
        //} else {
        //    float smoothHeight = Mathf.SmoothDamp(size.y, TotalHeight, ref smoothSpeedVelocity, smoothSpeed);
        //    size.y = smoothHeight;

        //    IsAnimating = true;
        //}

        RT.sizeDelta = size;
    }
}
