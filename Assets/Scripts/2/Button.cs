using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
    ControllerLevel2 bar;
    [SerializeField]
    float increaseValue;

    void Start()
    {
        bar = GameObject.Find("ControllerLevel2").GetComponent<ControllerLevel2>();
    }

    void OnMouseDown()
    {
        bar.currentStatus += increaseValue;
    }
}
