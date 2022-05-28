using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CandleStats
{
    [Range(1,100)]
    public float MaxHP;
    [HideInInspector]
    public float HP;
    [Range(1, 100)]
    public float Power;
    [Range(1, 100)]
    public float RegenerateHP;
    [Range(1, 100)]
    public float DecayPerSec;
    
    // x = power, y = decay
    public List<Vector2Int> Mutltiplier;
    public List<int> MoodThreshold;
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

        SM.SetWorkingState(new W_working());
        SM.SetMoodState(new M_Happy());
        candleStats.HP = candleStats.MaxHP;
    }

    public void Decay()
    {
        candleStats.HP -= (candleStats.DecayPerSec + candleStats.Mutltiplier[SM.moodState.CurrentIndex].y) * Time.deltaTime;
    }

    public void Work(ProgressBar pb)
    {
        pb.currentProgress += (candleStats.Power + candleStats.Mutltiplier[SM.moodState.CurrentIndex].x) * Time.deltaTime;
        Debug.Log(name + " is working with power " + candleStats.Power + " " + candleStats.Mutltiplier[SM.moodState.CurrentIndex].x);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
