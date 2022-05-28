using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class CandleStats
{
    public float MaxHP;
    public float HP;
    public float Power;
    public float RegenerateHP;
    public float DecayPerSec;

    [Header("Crunch")]
    public float AdditionalPower;
    public float AdditionalDecay;

    [Header("Mood")]
    // x = power, y = decay
    public List<Vector2Int> Mutltiplier;
    public List<int> MoodThreshold;

    [Header("Beta Testing")]
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI CurrentMoodState;
    public TextMeshProUGUI CurrentWorkingState;

}

public class Candle : MonoBehaviour, IEntity
{
    public CandleStats candleStats;
     
    public StateMachine SM { get; private set; }
    public Candle currCandle { get; set; }

    private void Awake()
    {
        currCandle = this;
        SM = new StateMachine(this);
        SM.owner = this;

        SM.SetWorkingState(new W_Working());
        SM.SetMoodState(new M_Happy());
        candleStats.HP = candleStats.MaxHP;
    }

    public void Update()
    {
        DisplayText();
    }

    public void Decay()
    {
        candleStats.HP -= (candleStats.DecayPerSec + candleStats.Mutltiplier[SM.moodState.CurrentIndex].y) * Time.deltaTime;
    }
    public void CrunchDecay()
    {
        candleStats.HP -= (candleStats.DecayPerSec + candleStats.AdditionalDecay +  candleStats.Mutltiplier[SM.moodState.CurrentIndex].y) * Time.deltaTime;
    }

    public void Work(ProgressBar pb)
    {
        pb.currentProgress += (candleStats.Power + candleStats.Mutltiplier[SM.moodState.CurrentIndex].x) * Time.deltaTime;
        pb.UpdateVisuals();

    }

    public void CrunchWork(ProgressBar pb)
    {
        pb.currentProgress += (candleStats.Power + candleStats.AdditionalPower + candleStats.Mutltiplier[SM.moodState.CurrentIndex].x) * Time.deltaTime;
        pb.UpdateVisuals();
    }

    public void Regeneration()
    {
        if(candleStats.HP < candleStats.MaxHP)
        candleStats.HP += candleStats.RegenerateHP * Time.deltaTime;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void DisplayText()
    {
        candleStats.HPText.text = candleStats.HP + "";
        candleStats.CurrentMoodState.text = SM.moodState.Name + " ";
        candleStats.CurrentWorkingState.text = SM.workingState.Name + " ";
    }
}
