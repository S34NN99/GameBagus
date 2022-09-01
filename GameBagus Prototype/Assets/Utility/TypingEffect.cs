using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class TypingEffect : MonoBehaviour {
    [SerializeField] private float lettersPerSecond = 15;

    [TextArea(5, 10)]
    [SerializeField] private string _textToType;
    public string TextToType { get => _textToType; set => _textToType = value; }

    [SerializeField] private UnityEvent<string> updateTextCallback;
    [SerializeField] private UnityEvent onAnimStarted;
    [SerializeField] private UnityEvent onAnimSkipped;
    [SerializeField] private UnityEvent onAnimFinished;

    public bool IsTyping { get; private set; }
    private Coroutine typingCoroutine;

    public void StartThenSkipTyping() {
        if (IsTyping) {
            SkipTyping();
        } else {
            StartTyping();
        }
    }

    public void StartTyping() {
        StopTyping();
        typingCoroutine = StartCoroutine(TypeTextCoroutine());

        IEnumerator TypeTextCoroutine() {
            IsTyping = true;
            onAnimStarted.Invoke();
            char[] letters = TextToType.ToCharArray();

            string currentText = "";
            updateTextCallback.Invoke(currentText);
            yield return new WaitForSeconds(1 / lettersPerSecond);

            foreach (var letter in letters) {
                currentText += letter;
                updateTextCallback.Invoke(currentText);
                yield return new WaitForSeconds(1 / lettersPerSecond);
            }

            IsTyping = false;
            onAnimFinished.Invoke();
        }
    }

    public void SkipTyping() {
        if (IsTyping) {
            updateTextCallback.Invoke(TextToType);

            StopTyping();
            onAnimSkipped.Invoke();
        }
    }

    private void StopTyping() {
        if (typingCoroutine != null) {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;

            IsTyping = false;
            onAnimFinished.Invoke();
        }
    }
}
