using UnityEngine;
using System.Collections;

public class EndObject : MonoBehaviour 
{
    int total = 0;
    int points;
    GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (total == 3)
        {
            points = 91;
            gameController.totalScore = points;
            gameController.doneWithMiniGame = true;
            gameController.won = true;
            GameObject[] humans = GameObject.FindGameObjectsWithTag("BlueHuman");
            foreach (GameObject temp in humans)
            {
                temp.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BlueHuman")
        {
            Destroy(other.gameObject);
            total++;
        }
    }
}
