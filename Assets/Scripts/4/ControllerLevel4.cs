using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerLevel4 : MiniGameMainController
{
    [SerializeField]
    ButtonToConnect button;
    [SerializeField]
    Transform[] positionsOfButtons = new Transform[12];
    [SerializeField]
    public Color pressedButtonColour;
    public int pressedButtonNumber;
    public GameObject pressedButtonFirst;
    public bool pressedButton = false;
    int totalConnected = 0;
    List<int> excludedNumbers = new List<int>();
    Quaternion rotation;
    int level = 0, levelPlaying = 1, amountToShow = 8;
    
    public override void Update()
    {
        base.Update();
        switch(level)
        {
            case -1:
                {
                    break;
                }
            case 0:
                {
                    level = 2;
                    break;
                }
            case 1:
                {
                    level = 2;
                    amountToShow = 12;
                    break;
                }
            case 2:
                {
                    level = -1;
                    for (int i = 0; i < amountToShow; i++)
                    {
                        int number;
                        do
                        {
                            number = Random.Range(0, amountToShow);
                        }
                        while (excludedNumbers.Contains(number));
                        excludedNumbers.Add(number);

                        if (number < 4 || number == 8 || number == 9)
                        {
                            rotation = this.transform.rotation * Quaternion.Euler(0, 0, -90);
                        }
                        else
                        {
                            rotation = this.transform.rotation;
                        }

                        ButtonToConnect temp = (ButtonToConnect)Instantiate(button, positionsOfButtons[number].position, rotation);
                        temp.tag = "Connect";
                        if (i < 2)
                        {
                            temp.GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                        else if (i >= 2 && i < 4)
                        {
                            temp.GetComponent<SpriteRenderer>().color = Color.red;
                        }
                        else if (i >= 4 && i < 6)
                        {
                            temp.GetComponent<SpriteRenderer>().color = Color.yellow;
                        }
                        else if (i >= 6 && i < 8)
                        {
                            temp.GetComponent<SpriteRenderer>().color = Color.green;
                        }
                        else if (i >= 8 && i < 10)
                        {
                            temp.GetComponent<SpriteRenderer>().color = Color.magenta;
                        }
                        else
                        {
                            temp.GetComponent<SpriteRenderer>().color = Color.black;
                        }
                        temp.controller = this.gameObject.GetComponent<ControllerLevel4>();
                        temp.number = i;
                    }
                    level = -1;
                    break;
                }
        }
    }

    public void Calculate(Color color, int number, GameObject button)
    {
        pressedButton = false;
        if (color == pressedButtonColour && number != pressedButtonNumber)
        {
            totalConnected++;
            Vector3[] positions = new Vector3[2];
            positions[0] = pressedButtonFirst.transform.position;
            positions[1] = button.transform.position;
            pressedButtonFirst.GetComponent<LineRenderer>().SetPositions(positions);
            pressedButtonFirst.GetComponent<BoxCollider2D>().enabled = false;
            button.GetComponent<BoxCollider2D>().enabled = false;
            pressedButtonFirst.GetComponent<ButtonToConnect>().Reset();
        }
        else
        {
            pressedButtonFirst.GetComponent<ButtonToConnect>().Reset();
        }
        if(levelPlaying == 1 && totalConnected == 4)
        {
            levelPlaying++;
            level = 1;
            excludedNumbers.Clear();
            totalConnected = 0;
            GameObject[] toDestroy = GameObject.FindGameObjectsWithTag("Connect");
            for(int i = 0; i < toDestroy.Length; i++)
            {
                Destroy(toDestroy[i]);
            }
        }
        if(totalConnected == 6)
        {
            WinState(true, timer * 7, " ");
        }
    }
}
