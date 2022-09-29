using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HolidayAction : BaseCandleAction {

    protected override void Awake() {
        base.Awake();
    }

    public override void ActOn(Candle candle) {
        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(new W_Working());
        candle.SM.workingState.Enter(candle);
    }
}
