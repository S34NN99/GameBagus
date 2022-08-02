using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject page1;
    public GameObject page2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Login()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void Plus(int num1, int num2)
    {
        int num3 = num1 + num2;
    }

    public void SwitchTo(GameObject go)
    {
        go.SetActive(true);
    }

}
