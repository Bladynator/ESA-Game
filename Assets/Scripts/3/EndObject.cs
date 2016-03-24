using UnityEngine;
using System.Collections;

public class EndObject : MonoBehaviour 
{
    int total = 0;
    ControllerLevel3 controller;

    void Start()
    {
        controller = GameObject.Find("ControllerLevel3").GetComponent<ControllerLevel3>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BlueHuman")
        {
            Destroy(other.gameObject);
            total++;
            if (total == 3)
            {
                controller.Done();
            }
        }
    }
}
