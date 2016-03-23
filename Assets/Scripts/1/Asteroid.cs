using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        this.transform.Translate(speed, 0, 0);
    }

    void OnMouseDown()
    {
        StartCoroutine(changeColour());
    }

    IEnumerator changeColour()
    {
        speed = 0;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
