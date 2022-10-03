using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour {
    [SerializeField] private float _duration;
    public float Duration { get => _duration; set => _duration = value; }

    [SerializeField] private bool _isTicking = true;
    public bool IsTicking { get => _isTicking; set => _isTicking = value; }

    [SerializeField] private bool _defaultToMax = true;
    public bool DefaultToMax { get => _defaultToMax; set => _defaultToMax = value; }

    [SerializeField] private bool _startCountdownOnEnabled = true;
    public bool StartCountdownOnEnabled { get => _startCountdownOnEnabled; set => _startCountdownOnEnabled = value; }

    private Coroutine countdownCoroutine;
    private bool isCountingDown;

    [SerializeField] private UnityEvent _onTimerStarted;
    public UnityEvent OnTimerStarted => _onTimerStarted;

    [SerializeField] private UnityEvent<float> _updateProgressCallback;
    public UnityEvent<float> UpdateProgressCallback => _updateProgressCallback;

    [SerializeField] private UnityEvent _onTimerEnded;
    public UnityEvent OnTimerEnded => _onTimerEnded;

    [SerializeField] private UnityEvent _onTimerSkipped;
    public UnityEvent OnTimerSkipped => _onTimerSkipped;

    private void OnEnable() {
        if (StartCountdownOnEnabled) {
            StartCountdown();
        }
    }

    public void StartCountdown() {
        IsTicking = true;

        StopCoutdownIfAny();

        isCountingDown = true;
        countdownCoroutine = StartCoroutine(CountdownEnumerator());

        IEnumerator CountdownEnumerator() {
            OnTimerStarted.Invoke();
            yield return new WaitForEndOfFrame();

            float elapsedTime = 0;

            while (elapsedTime < Duration && IsTicking) {
                elapsedTime += Time.deltaTime;
                UpdateProgressCallback.Invoke(elapsedTime / Duration);

                yield return new WaitForEndOfFrame();
            }

            isCountingDown = false;
            OnTimerEnded.Invoke();
        }
    }

    private bool StopCoutdownIfAny() {
        if (isCountingDown) {
            StopCoroutine(countdownCoroutine);
            isCountingDown = false;

            if (DefaultToMax) {
                UpdateProgressCallback.Invoke(1f);
            } else {
                UpdateProgressCallback.Invoke(0f);
            }

            return true;
        }
        return false;
    }

    public void SkipCountdownIfAny() {
        if (StopCoutdownIfAny()) {
            OnTimerSkipped.Invoke();
        }
    }

    public void Pause() {
        IsTicking = false;
    }

    public void Resume() {
        IsTicking = true;
    }

    public void TogglePauseResume() {
        if (IsTicking) {
            Pause();
        } else {
            Resume();
        }
    }
}
