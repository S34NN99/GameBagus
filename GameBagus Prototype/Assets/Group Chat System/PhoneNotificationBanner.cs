
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PhoneNotificationBanner : MonoBehaviour {
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float pivotSmoothTime = 0.2f;
    private float currentPivotVelocity;
    private float targetPivotY;

    [SerializeField] private StringProperty[] notificationBannerContent;
    [SerializeField] private StringProperty[] popUpContent;

    private void Awake() {
        if (rectTransform == null) {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }
    }

    private void Update() {
        Vector2 anchor = rectTransform.pivot;
        if (anchor.y != targetPivotY) {
            anchor.y = Mathf.SmoothDamp(anchor.y, targetPivotY, ref currentPivotVelocity, pivotSmoothTime);

            rectTransform.pivot = anchor;
        }
    }

    public void Show() {
        targetPivotY = 1;

        foreach (var runtimeString in notificationBannerContent) {
            runtimeString.Value = ObservableVariable.ConvertToRuntimeText(runtimeString.Value);
        }
    }

    public void RefreshPopUpContent() {
        foreach (var runtimeString in popUpContent) {
            runtimeString.Value = ObservableVariable.ConvertToRuntimeText(runtimeString.Value);
        }
    }

    public void Hide() {
        targetPivotY = 0;
    }

    public void SelfDestructIn(float seconds) {
        Destroy(gameObject, seconds);
    }
}
