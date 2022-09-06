using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CandleHpAffector : MonoBehaviour {
    [SerializeField] private float duration = 5f;
    [SerializeField] private float HPAmount = 15f;

    public void StartHPAffector(CandleStats candleStats)
    {
        StartCoroutine(HPAffector(candleStats));
    }

    private IEnumerator HPAffector(CandleStats candleStats)
    {
        CalculateHP(candleStats.HpProp.Value, HPAmount);
        yield return new WaitForSeconds(duration);
    }

    private float CalculateHP(float candleHP, float amount)
    {
        return candleHP - amount;
    }
}
