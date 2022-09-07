using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerformanceReview : MonoBehaviour
{
    //[SerializeField] private PerformanceReviewContent _prcontent;
    //public PerformanceReviewContent PRContent => _prcontent;

    [SerializeField] private PaperContent _paperContent;
    public PaperContent PaperContent => _paperContent;

    [Space]
    [SerializeField] private TextMeshProUGUI projectName;
    [SerializeField] private Image appImage;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private TextMeshProUGUI signature;

    [Space]
    [SerializeField] private GameObject milestoneParent;
    [SerializeField] private GameObject favourParent;
    [SerializeField] private GameObject candlesParent;

    public void UpdateContent()
    {
        projectName.text = PaperContent.ProjectName;
        appImage.sprite = PaperContent.AppImage;
        content.text = PaperContent.Content;
        signature.text = PaperContent.Signature;
    }

    public void UpdateIcons()
    {
        UpdateMilestoneIcon();
        UpdateFavourIcon();
        UpdateCandleIcon();
    }

    private void UpdateMilestoneIcon()
    {

    }

    private void UpdateFavourIcon()
    {
       
    }

    private void UpdateCandleIcon()
    {
        CandleManager cm = FindObjectOfType<CandleManager>();
        int counter = cm.CheckRemainingCandles();

        for(int i = 0; i < counter; i++)
        {
            Image image= candlesParent.transform.GetChild(i).GetComponent<Image>();
            image.sprite = PaperContent.CandleIcon.IconColour;
        }
    }

    //just for showcase, remove this later
    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
