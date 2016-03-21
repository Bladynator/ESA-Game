using UnityEngine;
using System.Collections;

public class Satellite : MonoBehaviour 
{
    [SerializeField]
    ControllerLevel1 controller;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Asteroid")
        {
            controller.points -= 20;
            Destroy(other.gameObject);
        }
    }
}
