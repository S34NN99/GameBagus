using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Project : MonoBehaviour {
    [Space(20)]
    [Header("ProgressBar")]
    public float currentProgress;
    public TextMeshProUGUI progressText;
    public Slider progressBar;
    public Animator wokAnim;
    public int completedProjectCounter;
    public TextMeshProUGUI completedProjectCounterTxt;

    [SerializeField] private FloatProperty _progressPercentageProp;
    public FloatProperty ProgressPercentageProp => _progressPercentageProp;

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
    [Range(0, 1f)]
    [SerializeField] private float nearingProjecFinishThreshold = 0.8f;

    private bool isFinishing;

    //e Update is called once per frame
    private void Update() {

        foreach (var candle in candleManager.GetCandles()) {
            candle.currCandle.SM.UpdateStates(this);
        }
        ProgressPercentageProp.Value = Mathf.InverseLerp(0, requiredProgress, currentProgress);
    }

    public void UpdateVisuals() {

        if (currentProgress >= requiredProgress) {
            currentProgress = 0;
            completedProjectCounter++;
            completedProjectCounterTxt.text = completedProjectCounter.ToString();
            candleManager.CheckCandles();

            requiredProgress = GetRequiredProgressForNextProject();
            clock.ResetClock(Random.Range(minProjectDuration, maxProjectDuration));

            progressBar.value = (currentProgress / requiredProgress);
            //wokAnim.SetTrigger("WokLoop");

            GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnProjectFinishedEvent);

            isFinishing = false;
        } else if (currentProgress >= requiredProgress * nearingProjecFinishThreshold) {
            if (!isFinishing) {
                GeneralEventManager.Instance.BroadcastEvent(AudioManager.NearingProjectFinishedEvent);
            }
            isFinishing = true;
        }

        progressText.text = (Mathf.InverseLerp(0, requiredProgress, currentProgress) * 100).ToString("0.0") + " %";
    }

    private int GetRequiredProgressForNextProject() {
        return minReqProgress + (Random.Range(0, progressRandomisationSteps) * progressRandomisationInterval);
    }
}
