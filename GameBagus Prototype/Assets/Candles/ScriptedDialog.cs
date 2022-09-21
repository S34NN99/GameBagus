using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class ScriptedDialog : MonoBehaviour {
    [SerializeField] private CandleManager _cm;
    private CandleManager CM => _cm;

    [SerializeField] private Dialog[] dialogs;
    [SerializeField] private GroupChat groupChat;
    [SerializeField] private bool randomiseCandles;

    public void StartDialog() {
        IReadOnlyList<Candle> allCandles = CM.CandleSlots;

        IEnumerable<int> dialogCandleIds = dialogs.Where(x => {
            if (x.IsOverrideProfile) {
                return false;
            } else if (allCandles[x.CandleProfileId - 1] == null) {
                return false;
            }

            return true;
        }).Select(x => x.CandleProfileId - 1);
        IEnumerator<int> dialogCandleIdEnumerator = dialogCandleIds.GetEnumerator();

        foreach (var dialog in dialogs) {
            if (dialog.Message == "") {
                continue;
            }

            CandleProfile profile;
            if (dialog.IsOverrideProfile) {
                profile = dialog.OverrideProfile;
            } else {
                // actual id
                // todo make this delayed (even though group chat has a queue function already)
                if (!dialogCandleIdEnumerator.MoveNext()) {
                    dialogCandleIdEnumerator = dialogCandleIds.GetEnumerator();
                    dialogCandleIdEnumerator.MoveNext();
                }
                int candleId = dialogCandleIdEnumerator.Current;
                profile = allCandles[candleId].Profile;
            }

            if (dialog.IsPlayer) {
                ChatMessage chatMsg = groupChat.CreateMessage<ChatMessage>(groupChat.PlayerChatMessageTemplate);
                chatMsg.DisplayMessage(profile, dialog.Message);
            } else {
                groupChat.SendTextMessage(profile, dialog.Message);
            }
        }
        dialogCandleIdEnumerator.Dispose();
    }

    [System.Serializable]
    private class Dialog {
        [SerializeField] private int _candleProfileId;
        public int CandleProfileId => _candleProfileId;

        [SerializeField] private bool _isOverrideProfile;
        public bool IsOverrideProfile => _isOverrideProfile;

        [SerializeField] private CandleProfile _overrideProfile;
        public CandleProfile OverrideProfile => _overrideProfile;


        [SerializeField] private bool _isPlayer;
        public bool IsPlayer => _isPlayer;

        [SerializeField] private string _message;
        public string Message => _message;
    }
}
