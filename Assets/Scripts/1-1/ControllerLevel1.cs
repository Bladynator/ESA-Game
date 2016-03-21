using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControllerLevel1 : MonoBehaviour 
{
    [SerializeField]
    Asteroid asteroid;
    GameController gameController;
    Vector2 randomPosition;
    bool waitingForSpawn = false, minigameTimerWait = false;
    public int points = 100;
    [SerializeField]
    int maxSpawn = 50, timer = 20;
    [SerializeField]
    float[] randomSize = new float[2], randomSpawnSpeed = new float[2], randomSpeed = new float[2];
    bool end = false;
    
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

	void Update () 
	{
        if (!end)
        {
            if (!waitingForSpawn && maxSpawn > 0)
            {
                int randomHeight = UnityEngine.Random.Range(-4, 4);
                randomPosition = new Vector2(this.transform.position.x, randomHeight);
                float randomSpawnSpeedDone = UnityEngine.Random.Range(randomSpawnSpeed[0], randomSpawnSpeed[1]);
                StartCoroutine(waitForSec(randomSpawnSpeedDone, randomPosition));
            }

            if (!minigameTimerWait)
            {
                StartCoroutine(minigameTimer());
            }


            if (timer <= 0)
            {
                gameController.totalScore = points;
                gameController.doneWithMiniGame = true;
                gameController.won = true;
                end = true;
                GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
                foreach (GameObject temp in asteroids)
                {
                    temp.SetActive(false);
                }
            }

            if(points <= 0)
            {
                gameController.totalScore = points;
                gameController.doneWithMiniGame = true;
                gameController.won = false;
                end = true;
                GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
                foreach (GameObject temp in asteroids)
                {
                    temp.SetActive(false);
                }
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
        if (!end)
        {
            GUI.Label(new Rect(Screen.width / 2, 0, 40, 40), points.ToString());
            GUI.Label(new Rect(Screen.width / 2 + 60, 0, 140, 40), "Time Left: " + timer.ToString());
        }
    }

    IEnumerator waitForSec(float sec, Vector2 randomPosition)
    {
        waitingForSpawn = true;
        Asteroid asteroidTemp = (Asteroid)Instantiate(asteroid, randomPosition, this.transform.rotation);
        float randomSpeedDone = UnityEngine.Random.Range(-randomSpeed[0], -randomSpeed[1]);
        asteroidTemp.speed = randomSpeedDone;
        float randomSizeDone = UnityEngine.Random.Range(randomSize[0], randomSize[1]);
        asteroidTemp.transform.localScale = new Vector2(randomSizeDone, randomSizeDone);
        yield return new WaitForSeconds(sec);
        maxSpawn--;
        waitingForSpawn = false;
    }
}
