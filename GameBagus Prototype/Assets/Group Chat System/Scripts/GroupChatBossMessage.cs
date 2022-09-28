
using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GroupChatBossMessage : MonoBehaviour {
    [SerializeField] private UnityEvent<float> updateProgressCallback;
    [SerializeField] private UnityEvent onTimesUp;
    [SerializeField] private UnityEvent fireDefaultActionCallback;

    [SerializeField] private float duration;
    [SerializeField] private float timer;

    private void Update() {
        if (timer < duration) {
            timer += Time.deltaTime;
            if (timer > duration) {
                timer = duration;
                fireDefaultActionCallback.Invoke();
            }
        } else {
            timer = duration;
            return;
        }
        float progress = timer / duration;
        UpdateProgress(progress);
    }

    public void Skip() {
        timer = duration;
        updateProgressCallback.Invoke(1);
    }

    public void UpdateProgress(float progress) {
        if (progress == 1) {
            onTimesUp.Invoke();
        }
        updateProgressCallback.Invoke(progress);
    }
}
