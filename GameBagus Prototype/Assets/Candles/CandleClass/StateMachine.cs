using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class StateMachine {
    public MoodState moodState;
    public WorkingState workingState;
    public IEntity owner;

    public CandleStats.Modifier powerMod;
    public CandleStats.Modifier powerConst;
    public CandleStats.Modifier decayMod;
    public CandleStats.Modifier decayConst;

    public StateMachine(IEntity owner) {
        this.owner = owner;
        powerMod = owner.currCandle.Stats.AddPowerModifier(10, CandleStats.Modifier.Type.multiply, 3);
        powerConst = owner.currCandle.Stats.AddPowerModifier(10, CandleStats.Modifier.Type.constant, 1);
        decayMod = owner.currCandle.Stats.AddDecayModifier(10, CandleStats.Modifier.Type.multiply, 1);
        decayConst = owner.currCandle.Stats.AddDecayModifier(10, CandleStats.Modifier.Type.constant, 1);
    }

    public void SetWorkingState(WorkingState state) {
        if (workingState == null && state != null) {
            workingState = state;
            workingState.Enter(owner);
        }
    }

    public void SetMoodState(MoodState state) {
        if (moodState == null && state != null) {
            moodState = state;
            moodState.Enter(owner);
        }
    }

    public void UpdateStates(Project pb) {
        moodState.Update(owner, pb);
        workingState.Update(owner, pb);
    }
}
