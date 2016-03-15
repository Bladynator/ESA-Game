﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
    [SerializeField]
    Texture2D locked;
    public int activeScreen = 0;
    GameController gameController;
    int lastMiniGameNumberToShow = 9;
    int miniGameToLoad;
    [SerializeField]
    string[] trivia;
    GUIStyle smallFont;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        smallFont = gameController.smallFont;
    }

    void OnGUI()
    {
        if (activeScreen > 0)
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), "Back"))
            {
                GameObject.Find("Menu").GetComponent<Menu>().activeScreen--;
            }
        }
        switch (activeScreen)
        {
            case 0:
                {
                    if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 125, 50), "Build Satalite"))
                    {
                        SceneManager.LoadScene("BuildSatellite");
                    }
                    if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 3, 125, 50), "Start Mission"))
                    {
                        activeScreen = 1;
                    }
                    break;
                }
            case 1:
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 3, 125, 50), "AIM"))
                    {
                        activeScreen = 2;
                    }
                    if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 3, 125, 50), locked))
                    {
                        
                    }
                    if (GUI.Button(new Rect(Screen.width / 2 + 125, Screen.height / 3, 125, 50), locked))
                    {
                        
                    }
                    break;
                }
            case 2:
                {
                    int p = 0;
                    for(int i = 1 + (lastMiniGameNumberToShow - 9); i < lastMiniGameNumberToShow; i++)
                    {
                        ShowMiniGameButtons(i, p, lastMiniGameNumberToShow);
                        if(i == 4 || i == 12 || i == 20 || i == 28)
                        {
                            p = 1;
                        }
                    }
                    if (lastMiniGameNumberToShow != 33)
                    {
                        if (GUI.Button(new Rect(Screen.width / 2 + 125, Screen.height / 3, 125, 50), "Next Pack"))
                        {
                            lastMiniGameNumberToShow += 8;
                        }
                    }
                    if(lastMiniGameNumberToShow != 9)
                    {
                        if (GUI.Button(new Rect(10, Screen.height / 3, 125, 50), "Previous Pack"))
                        {
                            lastMiniGameNumberToShow -= 8;
                        }
                    }
                    break;
                }
            case -1:
                {
                    GUI.TextArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), trivia[miniGameToLoad], smallFont);
                    if (GUI.Button(new Rect(Screen.width - 125, Screen.height - 50, 125, 50), "Next"))
                    {
                        gameController.inGame = true;
                        SceneManager.LoadScene(miniGameToLoad.ToString());
                    }
                    break;
                }
        }

    }

    void ShowMiniGameButtons(int i, int p, int lastNumber)
    {
        int xOffset = i - (lastMiniGameNumberToShow - 9);
        if(p == 1)
        {
            xOffset -= 4;
        }
        if (GUI.Button(new Rect(100 + (xOffset * 75), Screen.height / 3 + (p * 50), 75, 50), i.ToString()))
        {
            activeScreen = -1;
            miniGameToLoad = i;
        }
    }
}
