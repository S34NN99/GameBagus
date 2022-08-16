
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PhoneNotificationBanner : MonoBehaviour {
    [Header("Ui")]
    [SerializeField] private UnityEvent<string> updateTitleCallback;
    [SerializeField] private UnityEvent<string> updateMessageCallback;

    private float displayTimer;

    private void Awake() {
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

        updateTitleCallback.Invoke(title);
        updateMessageCallback.Invoke(message);
    }
}
