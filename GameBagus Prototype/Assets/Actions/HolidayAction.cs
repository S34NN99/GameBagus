using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HolidayAction : BaseAction {

    public override void ActOn(Candle candle) {
        print(candle.name + " is on holidays");
    }
}
