using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using TMPro;

public class TextHeightFitter : MonoBehaviour {
    [SerializeField] private RectTransform targetRectTransform;

    [Header("Height")]
    [SerializeField] private float staticHeight;
    [SerializeField] private FlexibleTextHeight[] textTargets;

    [Header("Animation")]
    [SerializeField] private bool useUnscaledDeltaTime;
    [SerializeField] private float smoothTime = 4f;
    [SerializeField] private float smoothAccuracy = 0.05f;
    private float smoothSpeedVelocity;

    public bool IsAnimating { get; protected set; }

    [SerializeField] private bool _recalculateHeightOnUpdate = true;
    private bool RecalculateHeightOnUpdate => _recalculateHeightOnUpdate;

    public float TotalHeight { get; private set; }

    public float CurrentHeight => targetRectTransform.sizeDelta.y;

    private void Update() {
        if (RecalculateHeightOnUpdate) {
            RecalculateTextHeight();
        }

        IsAnimating = ReadjustTextHeight();
    }

    public void RecalculateTextHeight() {
        TotalHeight = textTargets.Sum(x => x.PreferredHeight) + staticHeight;

        IsAnimating = CurrentHeight != TotalHeight;
    }

    private bool ReadjustTextHeight() {
        if (CurrentHeight == TotalHeight) {
            return false;
        } else {
            float deltaTime = useUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;
            float adjustedHeight = Mathf.SmoothDamp(CurrentHeight, TotalHeight, ref smoothSpeedVelocity, 1 / smoothTime, 5000, deltaTime);
            if (Mathf.Abs(adjustedHeight - CurrentHeight) < smoothAccuracy) {
                adjustedHeight = TotalHeight;
            }

            SetHeightTo(adjustedHeight);

            return true;
        }
    }

    public void SetHeightTo(float height) {
        Vector2 sizeDelta = targetRectTransform.sizeDelta;
        sizeDelta.y = height;
        targetRectTransform.sizeDelta = sizeDelta;

        IsAnimating = CurrentHeight != TotalHeight;
    }

    [System.Serializable]
    private class FlexibleTextHeight {
        [SerializeField] private TextMeshProUGUI _textTarget;
        public TextMeshProUGUI TextTarget => _textTarget;

        [SerializeField] private float _minimumHeight;
        public float MinimumHeight => _minimumHeight;

        public float PreferredHeight => Mathf.Max(TextTarget.preferredHeight, MinimumHeight);
    }
}
