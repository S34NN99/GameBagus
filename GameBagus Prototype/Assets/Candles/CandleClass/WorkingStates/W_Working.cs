using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Working : WorkingState
{
    public override string Name => "Working";
    public override float FireSpeed => 0.4f;

    public override void Enter(IEntity entity)
    {
        entity.currCandle.SetFireSpeed(FireSpeed);
    }

    public override void Update(IEntity entity, ProgressBar pb)
    {
        entity.currCandle.Work(pb);
        entity.currCandle.Decay();
    }

    public override void Exit(IEntity entity)
    {
        entity.currCandle.SM.workingState = null;
    }
}
