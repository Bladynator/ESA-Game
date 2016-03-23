using UnityEngine;
using System.Collections;

public class MiniGameMainController : MonoBehaviour
{
    protected GameController gameController;
    protected int timer = 20;
    protected bool minigameTimerWait = false;
    protected bool timerAtZero = false;
    protected bool end = false;

    public virtual void Start ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	public virtual void Update ()
    {
        if (!minigameTimerWait)
        {
            if(timer <= 0)
            {
                timerAtZero = true;
            }
            StartCoroutine(minigameTimer());
        }
    }

    public IEnumerator minigameTimer()
    {
        minigameTimerWait = true;
        yield return new WaitForSeconds(1);
        timer--;
        minigameTimerWait = false;
    }

    public void WinState(bool won, int points, string tagToDestroy)
    {
        gameController.totalScore = points;
        gameController.doneWithMiniGame = true;
        gameController.won = won;
        end = true;
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag(tagToDestroy);
        foreach (GameObject temp in toDestroy)
        {
            temp.SetActive(false);
        }
    }

    void OnGUI()
    {
        if (!end)
        {
            GUI.Label(new Rect(Screen.width / 2, 0, 40, 40), points.ToString());
            GUI.Label(new Rect(Screen.width / 2 + 60, 0, 140, 40), "Time Left: " + timer.ToString());
        }
    }
}
