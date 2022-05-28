using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Crunch : WorkingState
{
    public override string Name => "Crunch";

    public override void Enter(IEntity entity)
    {
        Debug.Log("In active state " + Name);
    }

    public override void Update(IEntity entity, ProgressBar pb)
    {
        entity.currCandle.CrunchWork(pb);
        entity.currCandle.CrunchDecay();
    }

    public override void Exit(IEntity entity)
    {
        entity.currCandle.SM.workingState = null;
        Debug.Log("Exiting state " + Name);
    }
}
