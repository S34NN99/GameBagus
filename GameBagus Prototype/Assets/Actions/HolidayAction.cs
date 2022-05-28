using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HolidayAction : BaseAction {

    public override void ActOn(Candle candle) {
        print(candle.name + " is on holidays");
        candle.SM.workingState.Exit(candle);
        candle.SM.SetWorkingState(new W_Working());
        candle.SM.workingState.Enter(candle);
    }
}
