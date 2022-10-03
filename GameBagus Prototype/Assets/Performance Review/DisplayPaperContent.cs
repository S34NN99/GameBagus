using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DisplayPaperContent : MonoBehaviour {

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

    private void OnEnable() {
        UpdateContent();
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnProjectPrologue);
    }

    private void OnDisable() {
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.OnProjectStart);
    }

    public void UpdateContent() {
        projectName.text = PaperContent.ProjectName;
        appImage.sprite = PaperContent.AppImage;
        content.text = PaperContent.Content;
        signature.text = PaperContent.Signature;
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
                image.sprite = PaperContent.MilesteonIcon.IconColour;
            }
        }
    }

    private void UpdateFavourIcon() {
        Milestone milestone = FindObjectOfType<Milestone>();

        int milestonesPassed = 0;
        for (int i = 0; i < milestone.MilestoneConditions.Count; i++) {
            if (milestone.MilestoneConditions[i].Passed) {
                Image image = favourParent.transform.GetChild(i).GetComponent<Image>();
                image.sprite = PaperContent.FavourIcon.IconColour;
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
            image.sprite = PaperContent.CandleIcon.IconColour;
        }
    }

    //just for showcase, remove this later
    public void RestartLevel() {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(0);
    }

}
