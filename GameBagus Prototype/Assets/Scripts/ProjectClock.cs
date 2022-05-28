using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ProjectClock : MonoBehaviour {
    [SerializeField] private int _projectDuration;
    public int ProjectDuration {
        get => _projectDuration;
        private set {
            _projectDuration = value;

            clockImg.fillAmount = Mathf.InverseLerp(0, _projectDuration, TimeRemaining);
        }
    }

    [SerializeField] private int _timeRemaining;
    public int TimeRemaining {
        get => _timeRemaining;
        private set {
            _timeRemaining = value;
            if (TimeRemaining == 0) {
                onTimesUp.Invoke();
            }

            timerTxt.text = _timeRemaining.ToString();
            clockImg.fillAmount = Mathf.InverseLerp(0, ProjectDuration, _timeRemaining);
        }
    }

    [SerializeField] private UnityEvent onTimesUp;

    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private Image clockImg;

    private void Start() {
        InvokeRepeating("Tick", 1, 1);
    }

    public void Tick() {
        TimeRemaining--;
    }

    public void ResetClock(int newDeadline) {
        ProjectDuration = newDeadline;
        TimeRemaining = newDeadline;
    }

    public void ShortenDeadline(float shortenedPercentage) {
        shortenedPercentage = Mathf.Clamp01(shortenedPercentage);
        int shortenedAmt = (int)(TimeRemaining * shortenedPercentage);

        ProjectDuration -= shortenedAmt;
        TimeRemaining -= shortenedAmt;
    }
}
