
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PhoneNotificationBanner : MonoBehaviour {
    [SerializeField] private RectTransform rectTransform;

    [Space]
    [SerializeField] private float offsetYWhenHidden = 0;
    [SerializeField] private float offsetYWhenShown = -25f;

    [SerializeField] private float pivotYWhenHidden = 0;
    [SerializeField] private float pivotYWhenShown = 1;

    [SerializeField] private float animSmoothTime = 0.2f;
    private float currentPivotYVelocity;
    private float currentOffsetYVelocity;

    private float targetPivotY;
    private float targetOffsetY;

    private void Awake() {
        if (rectTransform == null) {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }
    }

    private void Update() {
        Vector2 anchor = rectTransform.pivot;
        Vector2 pos = rectTransform.anchoredPosition;
        if (anchor.y != targetPivotY || pos.y != targetOffsetY) {
            anchor.y = Mathf.SmoothDamp(anchor.y, targetPivotY, ref currentPivotYVelocity, animSmoothTime);
            pos.y = Mathf.SmoothDamp(pos.y, targetOffsetY, ref currentOffsetYVelocity, animSmoothTime);

            rectTransform.pivot = anchor;
            rectTransform.anchoredPosition = pos;
        }
    }

    public void Show() {
        targetPivotY = pivotYWhenShown;
        targetOffsetY = offsetYWhenShown;
    }

    public void Hide() {
        targetPivotY = pivotYWhenHidden;
        targetOffsetY = offsetYWhenHidden;
    }
}
