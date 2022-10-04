using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class Cutscene : MonoBehaviour {
    [SerializeField] private GameObject _parent;
    public GameObject Parent => _parent;


    [SerializeField] private RuntimeStringRefresher _mainBody;
    public RuntimeStringRefresher MainBody => _mainBody;

    [SerializeField] private SequentialActionLoop _actionLoop;
    public SequentialActionLoop ActionLoop => _actionLoop;

    [SerializeField] private UnityEvent _onCutsceneEnded;
    public UnityEvent OnCutsceneEnded => _onCutsceneEnded;

    private Queue<string> _pages = new Queue<string>();
    private Queue<string> Pages => _pages;

    public void GoToNextPage() {
        if (Pages.Any()) {
            MainBody.Text = Pages.Dequeue();
            MainBody.RefreshText();
            ActionLoop.JumpTo(0);
            ActionLoop.FireNextAction();
        } else {
            OnCutsceneEnded.Invoke();
        }
    }

    public void ClearContent() {
        Pages.Clear();
        MainBody.Text = "";
        MainBody.RefreshText();
    }

    public void QueuePage(IList<string> pages) {
        foreach (var page in pages) {
            Pages.Enqueue(page);
        }
    }
}
