using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class VacationAction : BaseCandleAction {

    protected override void Awake() {
        base.Awake();
    }

    public override void ActOn(Candle candle) {
        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(new W_Vacation());
    }
}
