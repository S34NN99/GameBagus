using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CandleStates
{
    Active,
    Inactive, 
    BurnOut
}

[System.Serializable]
public class CandleStats
{
    public CandleStates currentState;

    [Range(1,100)]
    public float HP;
    [Range(1, 100)]
    public float Power;
    [Range(1, 100)]
    public float DecayPerSec;
    [Range(1, 100)]
    public float CostPerPay; //reconsider naming
}

public class Candle : MonoBehaviour
{
    public CandleStats candleStats;

    // Update is called once per frame
    void Update()
    {
        UpdateState();

        // Move these into UpdateState
        Work();
        Decay();
    }

    void Work()
    {
        ProgressBar.instance.currentProgress += candleStats.Power * Time.deltaTime;
    }

    void Decay()
    {
        candleStats.HP -= candleStats.DecayPerSec * Time.deltaTime;

        if (candleStats.HP <= 0)
        {
            Death();
        }
    }

    void Death()
    {

    }

    void UpdateState()
    {
        switch(candleStats.currentState)
        {
            case CandleStates.Active:
                break;

            case CandleStates.Inactive:
                break;

            case CandleStates.BurnOut:
                break;
        }
    }
}
