
using System.Collections.Generic;
using System.Collections;

using UnityEngine;

[RequireComponent(typeof(ProjectEventTrigger))]
public class ManagementEvent : GameEventBase<GroupChat> {
    [Header("Content")]
    [SerializeField] [RuntimeString] private string _title;
    public string Title => ObservableVariable.ConvertToRuntimeText(_title);

    [SerializeField] [RuntimeString] private string _mainBody;
    public string MainBody => ObservableVariable.ConvertToRuntimeText(_mainBody);

    [SerializeField] [RuntimeString] private string _footer;
    public string Footer => ObservableVariable.ConvertToRuntimeText(_footer);

    [SerializeField] private float _displayDuration;
    public float DisplayDuration => _displayDuration;

    [SerializeField] private CandleProfile _bossProfile;
    public CandleProfile BossProfile => _bossProfile;

    [Header("Actions")]
    [Tooltip("First action is the default action")]
    [SerializeField] private ProjectEventAction[] _availableActions;
    public IReadOnlyList<ProjectEventAction> AvailableActions => _availableActions;
    public ProjectEventAction DefaultAction => AvailableActions[0];

#if UNITY_EDITOR
    // auto assign trigger
    protected override void OnValidate() {
        base.OnValidate();
    }
#endif

    protected override void Update() {
        base.Update();
    }

    protected override void TriggerEvent(GroupChat groupChat) {
        GroupChatBossMessage bossMessage = groupChat.CreateBossMessage();
        PhoneCallAlert phoneCallAlert = groupChat.PhoneCallAlert;

        bool phoneCallActivated = false;

        Coroutine timerCoroutine = StartCoroutine(CountdownEnumerator());

        foreach (var availableAction in AvailableActions) {
            availableAction.EventCallback.AddListener(DeactivateEvent);
        }

        bossMessage.DisplayMessage(BossProfile, this);
        bossMessage.MoreInfoButton.onClick.AddListener(() => {
            phoneCallActivated = true;

            phoneCallAlert.Show();
            phoneCallAlert.Message.DisplayMessage(BossProfile, MainBody);
            phoneCallAlert.SetActions(AvailableActions);
        });

        IEnumerator CountdownEnumerator() {
            float timer = DisplayDuration;

            while (timer > 0) {
                yield return new WaitForEndOfFrame();

                timer -= Time.deltaTime;

                float currentProgress = Mathf.InverseLerp(0, DisplayDuration, timer);
                bossMessage.UpdateProgress(currentProgress);

            }

            if (phoneCallActivated) {
                phoneCallAlert.Hide();
            }
            bossMessage.UpdateProgress(0f);
            DefaultAction.EventCallback.Invoke();
        }

        void DeactivateEvent() {
            if (timerCoroutine != null) {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }

            bossMessage.UpdateProgress(0f);
        }
    }
}
