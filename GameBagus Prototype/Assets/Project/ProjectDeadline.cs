using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class ProjectDeadline : MonoBehaviour {
    [SerializeField] private RectTransform deadlineIndicator;
    [SerializeField] private RectTransform deadlineProgressImg;
    private RectTransform deadlineProgressRect;

    [SerializeField] private UnityEvent<float> updateProgressPercentCallback;


    private void Awake() {
        deadlineProgressRect = deadlineProgressImg.GetComponent<RectTransform>();
    }

    private float deadlineProgressImgWidth => deadlineProgressRect.rect.width;

    public void SetTimeRemaining(float timeRemaining) {
        Vector2 currentPos = deadlineIndicator.anchoredPosition;
        currentPos.x = Mathf.Lerp(0, deadlineProgressImgWidth, 1 - timeRemaining);
        deadlineIndicator.anchoredPosition = currentPos;

        updateProgressPercentCallback.Invoke(timeRemaining);
    }
}
