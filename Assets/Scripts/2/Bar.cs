using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bar : MonoBehaviour 
{
    public float currentStatus = 0;
    [SerializeField]
    Texture2D emptyBar, fullBar;
    [SerializeField]
    float reduceValue;
    [SerializeField]
    int timer;
    bool minigameTimerWait = false, done = false;
    GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        if (!done)
        {
            if (currentStatus > 0)
            {
                currentStatus -= reduceValue;
            }

            if (!minigameTimerWait)
            {
                StartCoroutine(minigameTimer());
            }
        }
    }

    IEnumerator minigameTimer()
    {
        minigameTimerWait = true;
        yield return new WaitForSeconds(1);
        timer--;
        minigameTimerWait = false;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 2 - 77, Screen.height / 5 - 2, 150, 29), emptyBar);
        if (!done)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - 77, Screen.height / 5 - 2, currentStatus, 29), fullBar);
            GUI.Label(new Rect(Screen.width / 2, 0, 150, 25), "Time Left: " + timer.ToString());
        }

        if(currentStatus >= 150 && !done)
        {
            done = true;
            gameController.totalScore = timer * 20;
            gameController.doneWithMiniGame = true;
            gameController.won = true;
        }
    }
}
