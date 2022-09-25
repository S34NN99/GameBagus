using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CrunchAction : BaseCandleAction {

    protected override void Awake() {
        base.Awake();
    }

    public override void ActOn(Candle candle) {
        print(candle.name + " is crunching");
        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(new W_Crunch());
    }
}