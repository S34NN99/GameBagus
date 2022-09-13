using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class W_Vacation : WorkingState {
    public override string Name => "Vacation";
    public override float FireSpeed => 0.2f;

    private CandleStats.Modifier healMod;
    private CandleStats.Modifier decayMod;

    public override void Enter(IEntity entity) {
        entity.currCandle.SetFireSpeed(FireSpeed);
        GeneralEventManager.Instance.BroadcastEvent(BossQuotes.OnCandleVacationEvent);
        //entity.currCandle.SM.powerConst.Strength = 0;
        //entity.currCandle.SM.decayConst.Strength = 0;

        healMod = entity.currCandle.Stats.AddPowerModifier(5, CandleStats.Modifier.Type.constant, 0);
        decayMod = entity.currCandle.Stats.AddDecayModifier(5, CandleStats.Modifier.Type.constant, -4);
    }

    public override void Update(IEntity entity, Project pb) {
        //entity.currCandle.Regeneration();
        entity.currCandle.Decay();
    }

    public override void Exit(IEntity entity) {
        CheckHP(entity);
        entity.currCandle.SM.workingState = null;
        entity.currCandle.Stats.RemovePowerModifier(healMod);
        healMod = null;
        entity.currCandle.Stats.RemoveDecayModifier(decayMod);
        decayMod = null;
    }

    public void CheckHP(IEntity entity) {
        IReadOnlyList<int> threshold = entity.currCandle.Stats.MoodThreshold;
        //List<int> threshold = entity.currCandle.candleStats.MoodThreshold;
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
        //float threshold = entity.currCandle.candleStats.MoodThreshold[num];
        //return entity.currCandle.candleStats.HP > threshold;
        float threshold = entity.currCandle.Stats.MoodThreshold[num];
        return entity.currCandle.Stats.HpProp.Value > threshold;
    }
}
