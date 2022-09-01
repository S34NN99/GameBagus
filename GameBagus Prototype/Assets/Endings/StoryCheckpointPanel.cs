
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Obsolete]
public class StoryCheckpointPanel : MonoBehaviour {
    [SerializeField] private GameObject rootPanel;

    [Header("Control buttons")]
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject exitCheckpointButton;

    [Header("Content UI")]
    [SerializeField] private Image pageMainImage;
    [SerializeField] private UnityEvent<string> updateMainBodyTextCallback;

    public StoryCheckpoint CurrentStoryCheckpoint { get; private set; }

    private int currentPageInCheckpoint;

    private void Awake() {
        rootPanel.SetActive(false);
    }

    public void ShowPanel(StoryCheckpoint storyCheckpoint) {
        if (storyCheckpoint == null) {
            HidePanel();
        }

        CurrentStoryCheckpoint = storyCheckpoint;
        currentPageInCheckpoint = 0;

        SetContentToPage(CurrentStoryCheckpoint.StoryPages[currentPageInCheckpoint]);
        EnableControlButtons();

        rootPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void HidePanel() {
        if (CurrentStoryCheckpoint == null) return;

        CurrentStoryCheckpoint = null;
        rootPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void GoToNextPage() {
        currentPageInCheckpoint++;

        StoryCheckpoint.Page currentPage = CurrentStoryCheckpoint.StoryPages[currentPageInCheckpoint];
        SetContentToPage(currentPage);

        EnableControlButtons();

    }
    public void GoToPreviousPage() {
        currentPageInCheckpoint--;

        StoryCheckpoint.Page currentPage = CurrentStoryCheckpoint.StoryPages[currentPageInCheckpoint];
        SetContentToPage(currentPage);

        EnableControlButtons();
    }

    private void SetContentToPage(StoryCheckpoint.Page currentPage) {
        pageMainImage.sprite = currentPage.MainGraphics_Image;
        updateMainBodyTextCallback.Invoke(currentPage.MainBodyText);

    }

    private void EnableControlButtons() {
        if (currentPageInCheckpoint >= CurrentStoryCheckpoint.StoryPages.Length - 1) {
            nextButton.SetActive(false);
            exitCheckpointButton.SetActive(true);
        } else {
            nextButton.SetActive(true);
            exitCheckpointButton.SetActive(false);
        }

        if (currentPageInCheckpoint == 0) {
            backButton.SetActive(false);
        } else {
            backButton.SetActive(true);
        }
    }
}
