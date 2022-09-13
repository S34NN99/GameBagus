using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour {
    [SerializeField] private float delay;
    public void Destroy(GameObject target) {
        Destroy(target, delay);
    }
}
