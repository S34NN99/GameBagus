using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Project : MonoBehaviour {
    [Header("ProgressBar")]
    [SerializeField] private FloatProperty _progressProp;
    public FloatProperty ProgressProp => _progressProp;

    [SerializeField] private FloatProperty _progressPercentProp;
    public FloatProperty ProgressPercentProp => _progressPercentProp;

    [SerializeField] private FloatProperty _elapsedTimeProp;
    public FloatProperty ElapsedTimeProp => _elapsedTimeProp;

    [SerializeField] private FloatProperty _timeRemainingPercentProp;
    public FloatProperty TimeRemainingPercentProp => _timeRemainingPercentProp;

    [SerializeField] private int requiredProgress = 240;
    public int RequiredProgress => requiredProgress;

    [Space]
    [SerializeField] private CandleManager candleManager;

    [Space]
    [SerializeField] private float projectDeadline = 50;

    [Range(0, 1f)]
    [SerializeField] private float nearingProjecFinishThreshold = 0.8f;

    [Space]
    //[SerializeField] private UnityEvent<float> updateProgressSliderCallback;
    //[SerializeField] private UnityEvent<float> updateProjectTimeRemaining;
    [SerializeField] private UnityEvent onProjectEnded;
    [SerializeField] private UnityEvent onDeadlineEnded;

    private bool isWorkingOnProject;
    private bool isFinishing;
    private float remainingTime = 0f;

    //e Update is called once per frame
    private void Update() {
        if (isWorkingOnProject) {
            foreach (var candle in candleManager.GetCandles()) {
                candle.currCandle.SM.UpdateStates(this);
            }

            UpdateVisuals();

            ElapsedTimeProp.Value += Time.deltaTime;
            remainingTime = 1 - (ElapsedTimeProp.Value / projectDeadline);

            ProgressPercentProp.Value = ProgressProp.Value / requiredProgress;
            TimeRemainingPercentProp.Value = remainingTime;


            //updateProgressSliderCallback.Invoke(ProgressProp.Value / requiredProgress);
            //updateProjectTimeRemaining.Invoke(remainingTime);

            if (remainingTime <= 0) {
                onDeadlineEnded?.Invoke();
            }
        }
    }

    public void ResumeWork() {
        isWorkingOnProject = true;
    }

    public void PauseWork() {
        isWorkingOnProject = false;
    }

    public void UpdateVisuals() {
        if (ProgressProp.Value >= requiredProgress) {
            onProjectEnded.Invoke();

            ProgressProp.Value = 0;
            candleManager.CheckCandles();

            //_completedProjectProp.Value++;
            //requiredProgress = GetRequiredProgressForNextProject();
            //clock.ResetClock(Random.Range(minProjectDuration, maxProjectDuration));

            GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnProjectFinishedEvent);

            isWorkingOnProject = false;
            isFinishing = false;
        } else if (ProgressProp.Value >= requiredProgress * nearingProjecFinishThreshold) {
            if (!isFinishing) {
                GeneralEventManager.Instance.BroadcastEvent(AudioManager.NearingProjectFinishedEvent);
            }
            isFinishing = true;
        }
    }

    //private int GetRequiredProgressForNextProject() {
    //    return minReqProgress + (Random.Range(0, progressRandomisationSteps) * progressRandomisationInterval);
    //}
}
