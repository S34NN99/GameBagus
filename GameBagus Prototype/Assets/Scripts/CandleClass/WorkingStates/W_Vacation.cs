using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Vacation : WorkingState
{
    public override string Name => "Vacation";

    public override void Enter(IEntity entity)
    {
        Debug.Log("In active state " + Name);
    }

    public override void Update(IEntity entity, ProgressBar pb)
    {
        entity.currCandle.Regeneration();
    }

    public override void Exit(IEntity entity)
    {
        Debug.Log("Exiting state " + Name);
    }

    public void CheckHP(IEntity entity)
    {
        List<int> threshold = entity.currCandle.candleStats.MoodThreshold;

        for(int i = 0; i < threshold.Count; i++)
        {
            if(CalculateThreshold(entity, i))
            {
                entity.SM.moodState.Exit(entity);
                switch (i)
                {
                    case (int)MoodStatesIndex.Happy:
                        entity.SM.SetMoodState(new M_Happy());
                        break;

                    case (int)MoodStatesIndex.Neutral:
                        entity.SM.SetMoodState(new M_Neutral());
                        break;

                    case (int)MoodStatesIndex.Sad:
                        entity.SM.SetMoodState(new M_Sad());
                        break;
                }
            }
        }
    }

    public bool CalculateThreshold(IEntity entity, int num)
    {
        float threshold = entity.currCandle.candleStats.MaxHP * entity.currCandle.candleStats.MoodThreshold[num] / 100;
        return entity.currCandle.candleStats.HP < threshold;
    }
}
