
using UnityEngine;
using TMPro;

public class PhoneNotificationBanner : MonoBehaviour {
    [Header("Ui Components")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float titleTextHeight = 75f;
    [SerializeField] private float minimumTextHeight = 125f;
    public float TextHeight { get; private set; }
    public float TotalHeight { get; private set; }

    public RectTransform RT { get; private set; }

    [Space]
    [SerializeField] private float popupAnimDuration = 0.5f;
    private float popupAnimTimer;
    private float displayTimer;

    private void Awake() {
        RT = GetComponent<RectTransform>();

        gameObject.SetActive(false);
    }

    private void Update() {
        if (displayTimer > 0) {
            displayTimer -= Time.deltaTime;
            if (displayTimer < 0) {
                displayTimer = 0;

                gameObject.SetActive(false);
            }
        }
    }

    public void DisplayNotification(string title, string message, float duration) {
        gameObject.SetActive(true);

        displayTimer = duration;

        titleText.text = title;
        messageText.text = message;

        ReadjustTextHeight();
        RT.sizeDelta = new Vector2(RT.sizeDelta.x, TotalHeight);
    }

    private void ReadjustTextHeight() {
        TextHeight = Mathf.Max(minimumTextHeight, messageText.preferredHeight);
        TotalHeight = TextHeight + titleTextHeight;
    }
}
