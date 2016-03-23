using UnityEngine;
using System.Collections;

public class RightLeft : MonoBehaviour
{
    RotateSatellite rotate;
    bool pressed = false;
    [SerializeField]
    float amount;

    void Start()
    {
        rotate = GameObject.Find("SatelliteToRotate").GetComponent<RotateSatellite>();
    }

    void Update()
    {
        if (pressed)
        {
            rotate.transform.rotation *= Quaternion.Euler(0, 0, amount);
        }
    }

    void OnMouseDown()
    {
        pressed = true;
    }

    void OnMouseUp()
    {
        pressed = false;
    }
}
