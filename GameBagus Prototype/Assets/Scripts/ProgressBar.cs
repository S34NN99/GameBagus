using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour {
    [Space(20)]
    [Header("ProgressBar")]
    public float currentProgress;
    public TextMeshProUGUI progressText;
    public Animator wokAnim;

    [SerializeField] private int minReqProgress;
    [SerializeField] private int reqProgressVarianceSteps;
    [Tooltip("The value of each step when randomising the value of the required progress for the next project")]
    [SerializeField] private int reqProgressVarianceIntervals;
    [SerializeField] private int requiredProgress;

    [Space(20)]
    [Header("Candle")]
    [SerializeField] private CandleManager candleManager;

    [Space(20)]
    [Header("Clock")]
    [SerializeField] private ProjectClock clock;


    // Update is called once per frame
    void Update() {

        foreach (var candle in candleManager.GetCandles()) {
            candle.currCandle.SM.UpdateStates(this);
        }
    }

    public void UpdateVisuals() {

        if (currentProgress >= requiredProgress) {
            currentProgress = 0;
            candleManager.CheckCandles();

            requiredProgress = GetRequiredProgressForNextProject();
            clock.ResetClock(clock.ProjectDuration);

            wokAnim.SetTrigger("WokLoop");
        }

        progressText.text = (Mathf.InverseLerp(0, requiredProgress, currentProgress) * 100).ToString("0.0") + " %";
    }

    private int GetRequiredProgressForNextProject() {
        return minReqProgress + (Random.Range(0, reqProgressVarianceSteps) * reqProgressVarianceIntervals);
    }
}
