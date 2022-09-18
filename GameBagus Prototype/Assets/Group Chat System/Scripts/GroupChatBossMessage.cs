﻿
using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GroupChatBossMessage : MonoBehaviour {
    [SerializeField] private ChatMessage _chatMessage;
    public ChatMessage ChatMessage => _chatMessage;

    [SerializeField] private Button _moreInfoButton;
    public Button MoreInfoButton => _moreInfoButton;

    [Header("UI")]
    [SerializeField] private UnityEvent<float> updateProgressCallback;
    [SerializeField] private UnityEvent<string> updateTitleCallback;
    [SerializeField] private UnityEvent<string> updateFooterCallback;
    [SerializeField] private UnityEvent onTimesUp;

    //public float Timer { get; private set; }

    public void DisplayMessage(CandleProfile profile, string title, string mainBody, string footer) {
        ChatMessage.DisplayMessage(profile, mainBody);

        updateTitleCallback.Invoke(title);
        updateFooterCallback.Invoke(footer);

        //StartCoroutine(StartCountdownEnumerator());

        //IEnumerator StartCountdownEnumerator() {
        //    Timer = managementEvent.DisplayDuration;

        //    yield return new WaitWhile(() => {
        //        Timer -= Time.deltaTime;
        //        if (Timer <= 0) {
        //            Timer = 0;
        //        }

        //        float currentProgress = Mathf.InverseLerp(0, managementEvent.DisplayDuration, Timer);
        //        updateProgressCallback.Invoke(currentProgress);


        //        return Timer > 0;
        //    });

        //    managementEvent.DefaultAction.Fire();
        //    onTimesUp.Invoke();
        //}
    }

    public void UpdateProgress(float progress) {
        if (progress == 0) {
            onTimesUp.Invoke();
        }
        updateProgressCallback.Invoke(progress);
    }
}