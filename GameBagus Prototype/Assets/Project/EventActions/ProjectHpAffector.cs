using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectHpAffector : MonoBehaviour
{
    [SerializeField] private Project project;
    [SerializeField] private int reducedHP = 10;
    [SerializeField] private int duration;


    public void StartHpAffector()
    {
        StartCoroutine(IncreaseProjectHP());
    }

    private IEnumerator IncreaseProjectHP()
    {
        project.RequiredProgress += reducedHP;
        yield return new WaitForSeconds(duration);
        project.RequiredProgress -= reducedHP;
    }
}
