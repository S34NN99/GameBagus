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
    public int completedProjectCounter;

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
    [SerializeField] private ProjectClock clock;
    [SerializeField] private int minProjectDuration = 20;
    [SerializeField] private int maxProjectDuration = 60;


    // Update is called once per frame
    void Update() {

        foreach (var candle in candleManager.GetCandles()) {
            candle.currCandle.SM.UpdateStates(this);
        }
    }

    public void UpdateVisuals() {

        if (currentProgress >= requiredProgress) {
            currentProgress = 0;
            completedProjectCounter++;
            candleManager.CheckCandles();

            requiredProgress = GetRequiredProgressForNextProject();
            clock.ResetClock(Random.Range(minProjectDuration, maxProjectDuration));

            wokAnim.SetTrigger("WokLoop");
        }

        progressText.text = (Mathf.InverseLerp(0, requiredProgress, currentProgress) * 100).ToString("0.0") + " %";
    }

    private int GetRequiredProgressForNextProject() {
        return minReqProgress + (Random.Range(0, progressRandomisationSteps) * progressRandomisationInterval);
    }
}
