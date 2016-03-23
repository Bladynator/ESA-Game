using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
    Bar bar;
    [SerializeField]
    float increaseValue;

    void Start()
    {
        bar = GameObject.Find("Bar").GetComponent<Bar>();
    }

    void OnMouseDown()
    {
        bar.currentStatus += increaseValue;
    }
}
