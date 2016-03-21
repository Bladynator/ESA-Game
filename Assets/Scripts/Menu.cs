using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
    [SerializeField]
    Texture2D locked, back, next;
    [SerializeField]
    Texture2D[] levelImages = new Texture2D[33];
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
        if(gameController.toLevelSelect)
        {
            gameController.toLevelSelect = false;
            activeScreen = 1;
        }
        if(gameController.doneWithMiniGame)
        {
            gameController.doneWithMiniGame = false;
            activeScreen = -1;
            miniGameToLoad = gameController.lastMiniGame;
            miniGameToLoad++;
        }
    }

    void OnGUI()
    {
        if (activeScreen > 0)
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), back, smallFont))
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
            case -2:
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
            case 1:
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
                        if (GUI.Button(new Rect(Screen.width / 2 + 125, Screen.height / 3, 125, 50), next, smallFont))
                        {
                            lastMiniGameNumberToShow += 8;
                        }
                    }
                    if(lastMiniGameNumberToShow != 9)
                    {
                        if (GUI.Button(new Rect(10, Screen.height / 3, 125, 50), back, smallFont))
                        {
                            lastMiniGameNumberToShow -= 8;
                        }
                    }
                    break;
                }
            case -1:
                {
                    GUI.TextArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), trivia[miniGameToLoad], smallFont);
                    if (GUI.Button(new Rect(Screen.width - 125, Screen.height - 50, 125, 50), next, smallFont))
                    {
                        gameController.inGame = true;
                        gameController.lastMiniGame = miniGameToLoad;
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
        if (gameController.lastLevelCleared >= i - 1)
        {
            if (GUI.Button(new Rect(100 + (xOffset * 75), Screen.height / 3 + (p * 50), 75, 50), levelImages[i]))
            {
                activeScreen = -1;
                miniGameToLoad = i;
            }
        }
        else
        {
            if (GUI.Button(new Rect(100 + (xOffset * 75), Screen.height / 3 + (p * 50), 75, 50), locked))
            {
            }
        }
    }
}
