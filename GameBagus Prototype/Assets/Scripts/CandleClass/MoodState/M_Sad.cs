using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Sad : MoodState
{
    public override string Name => "Sad";

    public override int CurrentIndex => 2;

    public override void Enter(IEntity entity)
    {
        Debug.Log("Enter active state " + Name);
    }

    public override void Update(IEntity entity, ProgressBar pb)
    {
        Debug.Log("Updating active state " + Name);
        CheckHP(entity);
    }

    public override void Exit(IEntity entity)
    {
        Debug.Log("Exiting active state " + Name);
    }

    public override void CheckHP(IEntity entity)
    {
        if (CalculateThreshold(entity))
        {
            entity.currCandle.SM.moodState.Exit(entity);
            entity.currCandle.Death();
        }
    }

    public override bool CalculateThreshold(IEntity entity)
    {
        float threshold = entity.currCandle.candleStats.MaxHP * entity.currCandle.candleStats.MoodThreshold[CurrentIndex] / 100;
        return entity.currCandle.candleStats.HP < threshold;
    }

}
