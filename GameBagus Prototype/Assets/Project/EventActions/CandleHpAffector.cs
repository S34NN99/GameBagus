using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CandleHpAffector : MonoBehaviour {
    [SerializeField] private float duration = 5f;
    [SerializeField] private float reducedHP = 15f;
    [SerializeField] private CandleManager CM;

    public void StartHPAffector()
    {
        StartCoroutine(HPAffector(CM.RandomizeCandle().Stats));
    }

    private IEnumerator HPAffector(CandleStats candleStats)
    {
        CalculateHP(candleStats.HpProp.Value, reducedHP);
        yield return new WaitForSeconds(duration);
    }

    private float CalculateHP(float candleHP, float amount)
    {
        return candleHP - amount;
    }
}
