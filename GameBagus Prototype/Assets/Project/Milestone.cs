using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milestone : MonoBehaviour
{
    [System.Serializable]
    public class MilestoneCondition
    {
        [SerializeField] private bool _passed;
        public bool Passed => _passed;

        [SerializeField] private int _threshold;
        public int Threshold => _threshold;
    }

    [SerializeField] private GameObject progressbar;
    [SerializeField] private Icons flagIcon;
    [SerializeField] private GameObject mileStoneTemplate;
    [SerializeField] private List<MilestoneCondition> _milestoneConditions;
    public IReadOnlyList<MilestoneCondition> MilestoneConditions => _milestoneConditions;

    private void Start()
    {
        InitializedMileStone();
    }

    private void InitializedMileStone()
    {
        float progressSize = progressbar.GetComponent<RectTransform>().rect.width;

        foreach(MilestoneCondition ms in MilestoneConditions)
        {
            GameObject msGo = Instantiate(mileStoneTemplate, Vector2.zero, Quaternion.identity);
            //msGo.transform.parent = progressbar.transform;
            //Vector2 newPos = new Vector2(CalculateXPosition(progressSize, ms.Threshold), progressbar.transform.position.y);
            //msGo.transform.position = ;
        }
    }

    private float CalculateXPosition(float width, float threshold)
    {
        return ((threshold / width) * 100);
    }

}
