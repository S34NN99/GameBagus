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
        project.StartCoroutine(IncreaseProjectHP(project));
    }

    private IEnumerator IncreaseProjectHP(Project project) {
        project.RequiredProgress += ChangeInHp;
        yield return new WaitForSeconds(Duration);
        project.RequiredProgress -= ChangeInHp;
    }
}
