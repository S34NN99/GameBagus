using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum CandleStates {
    Active,
    Inactive,
    BurnOut
}

[System.Serializable]
public class CandleStats {
    public CandleStates currentState;

    [Range(1, 100)]
    public float HP;
    [Range(1, 100)]
    public float Power;
    [Range(1, 100)]
    public float RegenerateHP;
    [Range(1, 100)]
    public float DecayPerSec;
    [Range(1, 100)]
    public float CostPerPay; //reconsider naming
}

public class Candle : MonoBehaviour, IEntity {
    public CandleStats candleStats;

    public StateMachine SM { get; private set; }

    private void Awake() {
        SM = new StateMachine(this);
        SM.owner = this;
        SM.SetWorkingState(new W_Active());
        SM.SetMoodState(new M_Happy());
        //SM.SetMoodState();
    }

    void Update() {
        //SM.UpdateStates();
    }
}
