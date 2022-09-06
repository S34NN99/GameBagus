using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavourAffector : MonoBehaviour
{
    [SerializeField] private int reducedFavour = 1;

    //Change when favour is implemented
    public void StartFavourAffector(float favour)
    {
        favour -= reducedFavour;
    }
}
