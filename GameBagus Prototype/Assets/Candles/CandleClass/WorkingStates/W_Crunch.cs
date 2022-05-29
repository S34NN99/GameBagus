using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Crunch : WorkingState
{
    public override string Name => "Crunch";
    public override float FireSpeed => 1.4f;


    public override void Enter(IEntity entity)
    {
        entity.currCandle.SetFireSpeed(FireSpeed);
    }

    public override void Update(IEntity entity, ProgressBar pb)
    {
        entity.currCandle.CrunchWork(pb);
        entity.currCandle.CrunchDecay();
    }

    public override void Exit(IEntity entity)
    {
        entity.currCandle.SM.workingState = null;
    }
}
