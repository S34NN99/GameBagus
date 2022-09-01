using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ProjectDeadline : MonoBehaviour {
    [SerializeField] private RectTransform deadlineIndicator;
    [SerializeField] private Image deadlineProgress;

    private float progressUiWidth => deadlineProgress.GetComponent<RectTransform>().sizeDelta.x;

    private void Awake() {
        //progressUiWidth = deadlineProgress.GetComponent<RectTransform>().sizeDelta.x;
    }

    public void SetTimeRemaining(float timeRemaining) {
        Vector2 currentPos = deadlineIndicator.anchoredPosition;
        currentPos.x = Mathf.Lerp(0, 1058, 1 - timeRemaining);
        deadlineIndicator.anchoredPosition = currentPos;

        deadlineProgress.fillAmount = timeRemaining;
    }
}
