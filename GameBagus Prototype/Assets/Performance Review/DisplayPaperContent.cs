using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class DisplayPaperContent : MonoBehaviour {

    [SerializeField] private PaperContent _paperContentSucess;
    public PaperContent PaperContentSuccess => _paperContentSucess;

    [SerializeField] private PaperContent _paperContentFail;
    public PaperContent PaperContentFail => _paperContentFail;

    private PaperContent ToUsePC;
    [Space]
    [SerializeField] private TextMeshProUGUI projectName;
    [SerializeField] private TextMeshProUGUI projectName2;
    [SerializeField] private Image appImage;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private TextMeshProUGUI signature;

    [Space]
    [SerializeField] private GameObject milestoneParent;
    [SerializeField] private GameObject favourParent;
    [SerializeField] private GameObject candlesParent;

    [Space]
    public UnityEvent IsPassed;
    private bool isPassed = false;
    private void OnEnable() {

        ToUsePC = PaperContentFail;
        Milestone milestone = FindObjectOfType<Milestone>();
        foreach (Milestone.MilestoneCondition ms in milestone.MilestoneConditions)
        {
            if (ms.Passed)
            {
                ToUsePC = PaperContentSuccess;
                isPassed = true;
                break;
            }
        }

        UpdateContent(ToUsePC);
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnProjectPrologue);
    }

    private void OnDisable() {
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnProjectStart);
    }

    public void UpdateContent(PaperContent pc) {
        projectName.text = pc.ProjectName;
        appImage.sprite = pc.AppImage;
        content.text = pc.Content;
        signature.text = pc.Signature;

        if(projectName2)
            projectName2.text = pc.ProjectName;
    }

    public void UpdateIcons() {
        UpdateMilestoneIcon();
        UpdateFavourIcon();
        UpdateCandleIcon();
    }

    private void UpdateMilestoneIcon() {
        Milestone milestone = FindObjectOfType<Milestone>();

        for (int i = 0; i < milestone.MilestoneConditions.Count; i++) {
            if (milestone.MilestoneConditions[i].Passed) {
                Image image = milestoneParent.transform.GetChild(i).GetComponent<Image>();
                image.sprite = ToUsePC.MilesteonIcon.IconColour;
            }
        }
    }

    private void UpdateFavourIcon() {
        Milestone milestone = FindObjectOfType<Milestone>();

        int milestonesPassed = 0;
        for (int i = 0; i < milestone.MilestoneConditions.Count; i++) {
            if (milestone.MilestoneConditions[i].Passed) {
                Image image = favourParent.transform.GetChild(i).GetComponent<Image>();
                image.sprite = ToUsePC.FavourIcon.IconColour;
                milestonesPassed++;
            }
        }

        ObservableVariable.FindProperty<IntProperty>("Milestones Completed").Value = milestonesPassed;
    }

    private void UpdateCandleIcon() {
        CandleManager cm = FindObjectOfType<CandleManager>();
        int counter = cm.CheckRemainingCandles();

        for (int i = 0; i < counter; i++) {
            Image image = candlesParent.transform.GetChild(i).GetComponent<Image>();
            image.sprite = ToUsePC.CandleIcon.IconColour;
        }
    }

    //just for showcase, remove this later
    public void RestartLevel() {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(0);
    }

    public void LevelChanged()
    {
        if(isPassed)
        {
            IsPassed.Invoke();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void TutorialPassed()
    {
        PlayerPrefs.SetInt("CompletedTutorial", 1);
    }

}
