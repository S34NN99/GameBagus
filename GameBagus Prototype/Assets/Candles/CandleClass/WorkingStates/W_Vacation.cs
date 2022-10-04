using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class W_Vacation : WorkingState {
    public override string Name => "Vacation";
    public override float SimSpeed => 0.4f;
    public override float FireSize => 0.1f;
    public override float FireSpeed => 4f;

    private CandleStats.Modifier healMod;
    private CandleStats.Modifier decayMod;

    public override void Enter(IEntity entity) {
        entity.currCandle.SetFireDetails(SimSpeed, FireSize, FireSpeed);
        //GeneralEventManager.Instance.BroadcastEvent(BossQuotes.OnCandleVacationEvent);
        //entity.currCandle.SM.powerConst.Strength = 0;
        //entity.currCandle.SM.decayConst.Strength = 0;

        healMod = entity.currCandle.Stats.AddPowerModifier(5, CandleStats.Modifier.Type.constant, 0);
        decayMod = entity.currCandle.Stats.AddDecayModifier(5, CandleStats.Modifier.Type.constant, -4);
    }

    public override void Update(IEntity entity, Project pb) {
        //entity.currCandle.Regeneration();
        if (entity.currCandle.Stats.HpProp.Value <= entity.currCandle.Stats.MaxHp)
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
}
