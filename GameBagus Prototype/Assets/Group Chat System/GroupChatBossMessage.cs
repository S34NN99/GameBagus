
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.Events;

using TMPro;

public class GroupChatBossMessage : GroupChatMessage {
    [Header("Additional UI Components")]
    //[SerializeField] private Image timerBar;
    //[SerializeField] private TextMeshProUGUI issueTitleText;
    //[SerializeField] private TextMeshProUGUI footerText;

    [Header("UI Events")]
    [SerializeField] private UnityEvent<float> updateProgressCallback;
    [SerializeField] private UnityEvent<string> updateTitleCallback;
    [SerializeField] private UnityEvent<string> updateFooterCallback;

    private float timer;
    private Issue currentIssue = Issue.defaultIssue;

    protected override void Awake() {
        base.Awake();
    }

    protected override void Update() {
        base.Update();

        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer < 0) {
                timer = 0;
                // todo force
            }

            float currentProgress = Mathf.InverseLerp(0, currentIssue.Duration, timer);
            updateProgressCallback.Invoke(currentProgress);

            //timerBar.fillAmount = Mathf.InverseLerp(0, currentIssue.Duration, timer);
        }
    }

    public void SetIssue(Issue issue) {
        currentIssue = issue ?? Issue.defaultIssue;

        //issueTitleText.text = currentIssue.Title;
        updateTitleCallback.Invoke(currentIssue.Title);
        //footerText.text = currentIssue.Footer;
        updateFooterCallback.Invoke(currentIssue.Footer);

        timer = currentIssue.Duration;
    }

    public class Issue {
        public static Issue defaultIssue = new Issue() {
            Title = "Urgent Issue",
            Footer = "Danger",
            Duration = 2f,
        };

        public string Title { get; set; }
        public string Footer { get; set; }
        public float Duration { get; set; }
    }
}
