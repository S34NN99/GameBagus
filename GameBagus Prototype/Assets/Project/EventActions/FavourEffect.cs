using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Effects/Favour")]
public class FavourEffect : ScriptableObject {
    [SerializeField] private string _favourPropName = "Favours";
    private string FavourPropName => _favourPropName;

    [SerializeField] private int _favourCost = 1;
    private int FavourCost => _favourCost;

    public void CheckCost(Button targetButton) {
        IntProperty favourProp = ObservableVariable.FindProperty<IntProperty>(FavourPropName);
        if (favourProp.Value < FavourCost) {
            targetButton.interactable = false;
        } else {
            targetButton.interactable = true;
        }
    }

    public void UseFavours() {
        IntProperty favourProp = ObservableVariable.FindProperty<IntProperty>(FavourPropName);
        favourProp.Value -= FavourCost;
    }
}
