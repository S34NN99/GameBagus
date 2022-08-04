using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class W_Vacation : WorkingState {
    public override string Name => "Vacation";
    public override float FireSpeed => 0.2f;

    public override void Enter(IEntity entity) {
        entity.currCandle.SetFireSpeed(FireSpeed);
        GeneralEventManager.Instance.BroadcastEvent(BossQuotes.OnCandleVacationEvent);
    }

    public override void Update(IEntity entity, ProgressBar pb) {
        entity.currCandle.Regeneration();
    }

    public override void Exit(IEntity entity) {
        CheckHP(entity);
        entity.currCandle.SM.workingState = null;
    }

    public void CheckHP(IEntity entity) {
        List<int> threshold = entity.currCandle.candleStats.MoodThreshold;
        for (int i = 0; i < threshold.Count; i++) {
            if (CalculateThreshold(entity, i)) {
                switch (i) {
                    case (int)MoodStatesIndex.Happy:
                        entity.SM.moodState.Exit(entity);
                        entity.SM.SetMoodState(new M_Happy());
                        return;

                    case (int)MoodStatesIndex.Neutral:
                        entity.SM.moodState.Exit(entity);
                        entity.SM.SetMoodState(new M_Neutral());
                        return;

                    case (int)MoodStatesIndex.Sad:
                        entity.SM.moodState.Exit(entity);

                        entity.SM.SetMoodState(new M_Sad());
                        return;
                }
            }
        }
    }

    public bool CalculateThreshold(IEntity entity, int num) {
        float threshold = entity.currCandle.candleStats.MoodThreshold[num];
        return entity.currCandle.candleStats.HP > threshold;
    }
}
