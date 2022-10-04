using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Project Hp")]
public class ProjectHpEffect : ScriptableObject {
    [SerializeField] private float _duration = 1;
    private float Duration => _duration;

    [SerializeField] private int _changeInHp = 1;
    private int ChangeInHp => _changeInHp;

    public void StartHpAffector(Project project) {
        if (project == null) {
            project = GameManager.Instance.Project;
        }
        project.StartCoroutine(IncreaseProjectHP(project));
    }

    private IEnumerator IncreaseProjectHP(Project project) {
        float elapsedTime = 0;
        float hpChangePerSec = ChangeInHp / Duration;

        while (elapsedTime < Duration) {
            elapsedTime += Time.deltaTime;
            project.ProgressProp.Value += hpChangePerSec * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
