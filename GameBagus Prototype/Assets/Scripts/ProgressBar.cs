using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public static ProgressBar instance = null;

    [Header("Dates")]
    public List<string> dates;
    public TextMeshProUGUI dateText;
    [SerializeField]
    private int dateCounter;
    public int updateTime;

    [Space(20)]
    [Header("ProgressBar")]
    public float currentProgress;
    public Slider progressSlider;

    List<Candle> candles;

    private void Awake()
    {
        instance = this;
        if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateDate", 0, updateTime);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        progressSlider.value = currentProgress;
    }

    void UpdateDate()
    {
        dateText.text = dates[dateCounter];
        dateCounter++;

        if(dateCounter >= dates.Count)
        {
            dateCounter = 0;
        }
    }
}
