using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControllerLevel1 : MiniGameMainController 
{
    [SerializeField]
    Asteroid asteroid;
    Vector2 randomPosition;
    bool waitingForSpawn = false;
    public int points = 100;
    [SerializeField]
    int maxSpawn = 50;
    [SerializeField]
    float[] randomSize = new float[2], randomSpawnSpeed = new float[2], randomSpeed = new float[2];
    
    public override void Start()
    {
        base.Start();
    }

    public override void Update () 
	{
        base.Update();
        if (!end)
        {
            if (!waitingForSpawn && maxSpawn > 0)
            {
                int randomHeight = Random.Range(-4, 4);
                randomPosition = new Vector2(this.transform.position.x, randomHeight);
                float randomSpawnSpeedDone = Random.Range(randomSpawnSpeed[0], randomSpawnSpeed[1]);
                StartCoroutine(waitForSec(randomSpawnSpeedDone, randomPosition));
            }
            
            if (timerAtZero)
            {
                WinState(true, points, "Asteroid");
            }

            if(points <= 0)
            {
                WinState(false, 0, "Asteroid");
            }
        }
	}

    IEnumerator waitForSec(float sec, Vector2 randomPosition)
    {
        waitingForSpawn = true;
        Asteroid asteroidTemp = (Asteroid)Instantiate(asteroid, randomPosition, this.transform.rotation);
        float randomSpeedDone = Random.Range(-randomSpeed[0], -randomSpeed[1]);
        asteroidTemp.speed = randomSpeedDone;
        float randomSizeDone = Random.Range(randomSize[0], randomSize[1]);
        asteroidTemp.transform.localScale = new Vector2(randomSizeDone, randomSizeDone);
        yield return new WaitForSeconds(sec);
        maxSpawn--;
        waitingForSpawn = false;
    }
}
