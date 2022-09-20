using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CandlePowerAffector : MonoBehaviour {
    [SerializeField] private int reducedPower = 1;
    [SerializeField] private float duration = 5f;
    [SerializeField] private GameObject candleParents;

    public void StartPowerAffector(int childNumber) {
        Candle candle = candleParents.transform.GetChild(childNumber - 1).GetComponent<Candle>();
        StartCoroutine(PowerAffector(candle));
    }

    private IEnumerator PowerAffector(Candle candle) {

        CandleStats.Modifier mod = candle.Stats.AddPowerModifier(10, CandleStats.Modifier.Type.constant, -reducedPower);
        yield return new WaitForSeconds(duration);
        candle.Stats.RemovePowerModifier(mod);

    }
}
