using UnityEngine;
using System.Collections;

public class RotateSatellite : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        this.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
        this.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
        this.GetComponent<LineRenderer>().SetColors(Color.red, Color.red);
        this.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, -45.5f, 1f));
    }
}
