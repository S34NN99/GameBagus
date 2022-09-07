using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class W_Working : WorkingState {
    public override string Name => "Working";
    public override float FireSpeed => 0.4f;

    public override void Enter(IEntity entity) {
        entity.currCandle.SetFireSpeed(FireSpeed);
        entity.currCandle.SM.powerConst.Strength = 2;
        entity.currCandle.SM.decayConst.Strength = 2;
    }

    public override void Update(IEntity entity, Project pb) {
        entity.currCandle.Work(pb);
        entity.currCandle.Decay();
    }

    public override void Exit(IEntity entity) {
        entity.currCandle.SM.workingState = null;
    }
}
