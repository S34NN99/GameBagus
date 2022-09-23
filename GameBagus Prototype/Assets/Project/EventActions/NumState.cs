using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Num State")]
public class NumState : ScriptableObject
{
    [SerializeField] private string _attribute;
    public string Attribute => _attribute;

    [SerializeField] private int _changeInNum;
    public int ChangeInNum => _changeInNum;


    public void IncreaseNumAttribute(MultipleEndingsSystem mes)
    {
        for(int i = 0; i < ChangeInNum; i++)
        {
            mes.IncrementNumState(Attribute);
        }
    }

    public void DecreaseNumAttribute(MultipleEndingsSystem mes)
    {
        for (int i = 0; i < ChangeInNum; i++)
        {
            mes.DecrementNumState(Attribute);
        }
    }
}
