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

    [SerializeField] private UnityEvent<float> updateProgressSliderCallback;
    [SerializeField] private int requiredProgress = 240;

    [Space]
    [SerializeField] private CandleManager candleManager;

    [Space]
    [SerializeField] private float projectDeadline = 50;
    [SerializeField] private FloatProperty elapsedTimeProp;
    [Range(0, 1f)]
    [SerializeField] private float nearingProjecFinishThreshold = 0.8f;

    [Space]
    [SerializeField] private UnityEvent<float> updateProjectTimeRemaining;
    [SerializeField] private UnityEvent onProjectEnded;

    private bool isWorkingOnProject;
    private bool isFinishing;

    //e Update is called once per frame
    private void Update() {
        if (isWorkingOnProject) {
            foreach (var candle in candleManager.GetCandles()) {
                candle.currCandle.SM.UpdateStates(this);
            }

            UpdateVisuals();

            elapsedTimeProp.Value += Time.deltaTime;
            updateProgressSliderCallback.Invoke(_progressProp.Value / requiredProgress);
            updateProjectTimeRemaining.Invoke(1 - (elapsedTimeProp.Value / projectDeadline));
        }
    }

    public void ResumeWork() {
        isWorkingOnProject = true;
    }

    public void PauseWork() {
        isWorkingOnProject = false;
    }

    public void UpdateVisuals() {
        if (_progressProp.Value >= requiredProgress) {
            onProjectEnded.Invoke();

            _progressProp.Value = 0;
            candleManager.CheckCandles();

            //_completedProjectProp.Value++;
            //requiredProgress = GetRequiredProgressForNextProject();
            //clock.ResetClock(Random.Range(minProjectDuration, maxProjectDuration));

            GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnProjectFinishedEvent);

            isWorkingOnProject = false;
            isFinishing = false;
        } else if (_progressProp.Value >= requiredProgress * nearingProjecFinishThreshold) {
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
