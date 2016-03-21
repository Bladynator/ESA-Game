using UnityEngine;
using System.Collections;

public class ButtonToConnect : MonoBehaviour
{
    public int number;
    Color color;
    public GameController4 controller;

    void Start()
    {
        color = this.GetComponent<SpriteRenderer>().color;
    }

    void OnMouseDown()
    {
        if (controller.pressedButton)
        {
            controller.Calculate(color, number, this.gameObject);
        }
        else
        {
            controller.pressedButtonColour = color;
            controller.pressedButtonNumber = number;
            controller.pressedButtonFirst = this.gameObject;
            controller.pressedButton = true;
            this.GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }

    public void Reset()
    {
        this.GetComponent<SpriteRenderer>().color = color;
    }
}
