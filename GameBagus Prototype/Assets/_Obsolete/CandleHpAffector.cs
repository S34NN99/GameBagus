using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CandleHpAffector : MonoBehaviour {
    [SerializeField] private float duration = 5f;
    [SerializeField] private float reducedHP = 15f;
    [SerializeField] private GameObject candleParents;

    public void StartHPAffector(int childNumber)
    {
        Candle candle = candleParents.transform.GetChild(childNumber - 1).GetComponent<Candle>();
        StartCoroutine(HPAffector(candle));
    }

    private IEnumerator HPAffector(Candle candle)
    {
        CalculateHP(candle.Stats.HpProp.Value, reducedHP);
        yield return new WaitForSeconds(duration);
    }

    private float CalculateHP(float candleHP, float amount)
    {
        return candleHP - amount;
    }
}
