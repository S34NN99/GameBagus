using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class TypingEffect : MonoBehaviour {
    [SerializeField] private float lettersPerSecond = 15;

    [TextArea(5, 10)]
    [SerializeField] private string _textToType;
    public string TextToType { get => _textToType; set => _textToType = value; }

    [SerializeField] private bool useUnscaledDeltaTime;

    [SerializeField] private UnityEvent<string> updateTextCallback;
    [SerializeField] private UnityEvent onAnimStarted;
    [SerializeField] private UnityEvent onAnimSkipped;
    [SerializeField] private UnityEvent onAnimFinished;

    public bool IsTyping { get; private set; }
    private Coroutine typingCoroutine;

    public void ResetTyping() {
        updateTextCallback.Invoke("");
        StopTyping(false);
    }

    public void StartTyping() {

        StopTyping(true);
        GeneralEventManager.Instance.BroadcastEvent(AudioManager.TypingEffect);

        typingCoroutine = StartCoroutine(TypeTextCoroutine());

        IEnumerator TypeTextCoroutine() {
            IsTyping = true;
            onAnimStarted.Invoke();
            char[] letters = TextToType.ToCharArray();

            string currentText = "";
            updateTextCallback.Invoke(currentText);
            float timeIntervalBetweenLetters = 1 / lettersPerSecond;

            if (useUnscaledDeltaTime) {
                yield return new WaitForSecondsRealtime(timeIntervalBetweenLetters);
            } else {
                yield return new WaitForSeconds(timeIntervalBetweenLetters);
            }

            foreach (var letter in letters) {
                currentText += letter;
                updateTextCallback.Invoke(currentText);

                if (useUnscaledDeltaTime) {
                    yield return new WaitForSecondsRealtime(timeIntervalBetweenLetters);
                } else {
                    yield return new WaitForSeconds(timeIntervalBetweenLetters);
                }
            }

            GeneralEventManager.Instance.BroadcastEvent(AudioManager.TypingEffectEnd);

            IsTyping = false;
            onAnimFinished.Invoke();
        }
    }

    public void SkipTyping() {
        if (IsTyping) {
            updateTextCallback.Invoke(TextToType);

            StopTyping(true);
            onAnimSkipped.Invoke();
        }
    }

    private void StopTyping(bool isFinished = false) {
        if (typingCoroutine != null) {
            GeneralEventManager.Instance.BroadcastEvent(AudioManager.TypingEffectEnd);

            StopCoroutine(typingCoroutine);
            typingCoroutine = null;

            IsTyping = false;
            if (isFinished) {
                onAnimFinished.Invoke();
            }
        }
    }
}
