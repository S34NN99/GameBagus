using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class W_Crunch : WorkingState {
    public override string Name => "Crunch";
    public override float FireSpeed => 1.4f;


    public override void Enter(IEntity entity) {
        entity.currCandle.SetFireSpeed(FireSpeed);
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnCandleCrunchEvent);
        entity.currCandle.SM.powerConst.Strength = 4;
        entity.currCandle.SM.decayConst.Strength = 4;
    }

    public override void Update(IEntity entity, Project pb) {
        entity.currCandle.CrunchWork(pb);
        entity.currCandle.CrunchDecay();
    }

    public override void Exit(IEntity entity) {
        entity.currCandle.SM.workingState = null;
    }
}
