using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Project : MonoBehaviour {
    [Space(20)]
    [Header("ProgressBar")]
    [SerializeField] private FloatProperty _progressProp;
    public FloatProperty ProgressProp => _progressProp;

    [SerializeField] private IntProperty _completedProjectProp;
    public IntProperty CompletedProjectProp => _completedProjectProp;

    [SerializeField] private UnityEvent<float> updateProgressSliderCallback;

    [Space(20)]
    [Header("Randomisation")]
    [SerializeField] private int minReqProgress = 240;
    [Tooltip("The number of steps when randomising the value of the required progress for the next project")]
    [SerializeField] private int progressRandomisationSteps = 29;
    [Tooltip("The value of each step when randomising the value of the required progress for the next project")]
    [SerializeField] private int progressRandomisationInterval = 10;
    [SerializeField] private int requiredProgress = 240;

    [Space(20)]
    [Header("Candle")]
    [SerializeField] private CandleManager candleManager;

    [Space(20)]
    [Header("Clock")]
    [SerializeField] private ProjectCalendar clock;
    [SerializeField] private int minProjectDuration = 20;
    [SerializeField] private int maxProjectDuration = 60;
    [SerializeField] private float projectMaxDuration = 50;
    [SerializeField] private float projectDuration = 50;
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

            projectDuration -= Time.deltaTime;
            updateProgressSliderCallback.Invoke(_progressProp.Value / requiredProgress);
            updateProjectTimeRemaining.Invoke(projectDuration / projectMaxDuration);
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
            _completedProjectProp.Value++;
            candleManager.CheckCandles();

            //requiredProgress = GetRequiredProgressForNextProject();
            //clock.ResetClock(Random.Range(minProjectDuration, maxProjectDuration));

            GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnProjectFinishedEvent);

            isFinishing = false;
        } else if (_progressProp.Value >= requiredProgress * nearingProjecFinishThreshold) {
            if (!isFinishing) {
                GeneralEventManager.Instance.BroadcastEvent(AudioManager.NearingProjectFinishedEvent);
            }
            isFinishing = true;
        }
    }

    private int GetRequiredProgressForNextProject() {
        return minReqProgress + (Random.Range(0, progressRandomisationSteps) * progressRandomisationInterval);
    }
}
