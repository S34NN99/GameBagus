using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Neutral : MoodState
{
    public override string Name => "Neutral";

    public override int CurrentIndex => (int)MoodStatesIndex.Neutral;

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
        entity.currCandle.SM.moodState = null;
        Debug.Log("Exiting active state " + Name);
    }

    public override void CheckHP(IEntity entity)
    {
        if (CalculateThreshold(entity))
        {
            entity.currCandle.SM.moodState.Exit(entity);
            entity.currCandle.SM.SetMoodState(new M_Sad());
        }
    }

    public override bool CalculateThreshold(IEntity entity)
    {
        float threshold = entity.currCandle.candleStats.MaxHP * entity.currCandle.candleStats.MoodThreshold[CurrentIndex] / 100;
        return entity.currCandle.candleStats.HP < threshold;
    }

}
