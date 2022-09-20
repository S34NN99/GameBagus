using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Milestone : MonoBehaviour {
    [System.Serializable]
    public class MilestoneCondition {
        [SerializeField] private bool _passed;
        public bool Passed {
            get => _passed;
            set { _passed = value; }
        }

        [SerializeField] private int _thresholdInPercentage;
        public int ThresholdInPercentage => _thresholdInPercentage;

        [SerializeField] private GameObject _self;
        public GameObject Self {
            get => _self;
            set { _self = value; }
        }
    }

    [SerializeField] private GameObject progressbar;
    [SerializeField] private Icons flagIcon;
    [SerializeField] private GameObject mileStoneTemplate;
    [SerializeField] private List<MilestoneCondition> _milestoneConditions;
    public IReadOnlyList<MilestoneCondition> MilestoneConditions => _milestoneConditions;

    private void Start() {
        InitializedMileStone();
    }

    private void InitializedMileStone() {
        float progressWidth = progressbar.GetComponent<RectTransform>().rect.width;

        foreach (MilestoneCondition ms in MilestoneConditions) {
            GameObject msGo = Instantiate(mileStoneTemplate, mileStoneTemplate.transform.parent);
            msGo.SetActive(true);
            ms.Self = msGo;
            float newPosX = CalculateXPosition(progressWidth, ms.ThresholdInPercentage);
            msGo.GetComponent<RectTransform>().localPosition = new Vector2(newPosX, msGo.transform.localPosition.y);
        }
    }

    public void CheckThreshold(float old_val, float new_Val) {
        foreach (MilestoneCondition ms in MilestoneConditions) {
            if (ms.Passed)
                continue;

            if ((new_Val * 100) >= ms.ThresholdInPercentage) {
                Debug.Log("Threshold Over");
                SetFlagOn(ms.Self);
                ms.Passed = true;
            }
        }
    }

    private void SetFlagOn(GameObject go) {
        go.GetComponentInChildren<Image>().sprite = flagIcon.IconColour;
    }

    private float CalculateXPosition(float width, float threshold) {
        return (threshold / 100f) * width;
    }
}
