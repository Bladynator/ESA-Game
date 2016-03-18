using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;

public class GameController : MonoBehaviour
{
    public bool inGame = false;
    bool paused = false;
    [SerializeField]
    Texture2D pauseButton, playButton;
    public GUIStyle smallFont;
    public int totalScore;
    public bool toLevelSelect = false, doneWithMiniGame = false, won = false;
    public int lastMiniGame;
    public bool doneWithAnimation = false;
    bool once = false;
    [SerializeField]
    AwesomeScript awesomeScript;
    [SerializeField]
    SaveLoad saveLoad;

    // Account Stats
    string NAMEFILE = "SaveDataPlayer";
    int[,] unlocks = new int[32, 3];
    int[,] pointsNeededForUnlock = new int[32, 3] { // 4 packs, each 8 minigames
    { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100},
    { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100},
    { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100},
    { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100}, { 80, 90, 100},};
    int lastLevelCleared;
    int[] highestScores = new int[32];
    // End 

    void Awake ()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int p = 0; p < 32; p++)
            {
                unlocks[p, i] = 0;
            }
        }
        for (int p = 0; p < 32; p++)
        {
            highestScores[p] = 0;
        }
        lastLevelCleared = 0;
        LoadData(saveLoad.readStringFromFile(NAMEFILE));
    }
    
    void OnGUI()
    {
        if (!doneWithMiniGame)
        {
            if (inGame && !paused)
            {
                if (GUI.Button(new Rect(0, 0, 50, 50), pauseButton, smallFont))
                {
                    paused = true;
                }
            }
        }
        if (paused)
        {
            Time.timeScale = 0.0f; // pause game
            if (GUI.Button(new Rect(0, 0, 50, 50), playButton, smallFont))
            {
                paused = false;
            }
            if (GUI.Button(new Rect(0, 100, 50, 50), "Back"))
            {
                paused = false;
                toLevelSelect = true;
                inGame = false;
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            Time.timeScale = 1; // unpause game
        }

        if(doneWithMiniGame && !once)
        {
            once = true;
            Instantiate(awesomeScript);
            if (totalScore > highestScores[lastMiniGame])
            {
                CheckUnlocks(lastMiniGame, totalScore);
                SaveData(unlocks, lastLevelCleared, highestScores);
            }
        }

        if(doneWithAnimation && doneWithMiniGame)
        {
            //GUI.TextArea(new Rect(0, 0, Screen.width, Screen.height),"");
            if (GUI.Button(new Rect(0, Screen.height / 2 - 30, 150, 60), "Back"))
            {
                once = false;
                paused = false;
                toLevelSelect = true;
                inGame = false;
                SceneManager.LoadScene("MainMenu");
            }
            if (won)
            {
                //GUI.Label(new Rect(Screen.width / 2, 50, 100, 50), "You Win!!!");
                GUI.Label(new Rect(Screen.width / 2, Screen.height / 4, 100, 50), totalScore.ToString());
                if (GUI.Button(new Rect(Screen.width / 1.3f, Screen.height / 2, 150, 50), "Next Level"))
                {
                    once = false;
                    Time.timeScale = 1;
                    SceneManager.LoadScene("MainMenu");
                }
            }
            else
            {
                GUI.Label(new Rect(Screen.width / 2, 50, 100, 50), "Try Again");
                if (GUI.Button(new Rect(Screen.width / 1.3f, Screen.height / 2, 150, 50), "Restart"))
                {
                    once = false;
                    Time.timeScale = 1;
                    doneWithMiniGame = false;
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }

    void CheckUnlocks(int level, int score)
    {
        for (int i = 0; i < 3; i++)
        {
            if (score >= pointsNeededForUnlock[level, i] && pointsNeededForUnlock[level, i] != 1)
            {
                unlocks[level, i] = 1;
                PlayUnlockAnimation();
            }
        }
    }

    IEnumerator PlayUnlockAnimation()
    {
        yield return new WaitForSeconds(2);
    }

    void SaveData(int[,] unlocks, int lastLevel, int[] highestScores)
    {
        // format: unlocks                                  lastLevel           highestScores
        // format: 0e0e0.. <e> 0e0e0.. <e> 0e0e0..  <BR> 1              <BR>    0e0e0e0e0...
        lastLevel--;
        string SAVEDATA = "";
        for (int i = 0; i < 3; i++)
        {
            for (int p = 0; p < 32; p++)
            {
                SAVEDATA += unlocks[p, i].ToString();
                if (p != 31)
                {
                    SAVEDATA += "e";
                }
            }
            SAVEDATA += "<e>";
        }
        SAVEDATA += "<BR>" + lastLevel.ToString() + "<BR>";
        for (int p = 0; p < 32; p++)
        {
            SAVEDATA += highestScores[p].ToString();
            if (p != 31)
            {
                SAVEDATA += "e";
            }
        }
        saveLoad.writeStringToFile(SAVEDATA, NAMEFILE);
    }

    void LoadData(string data)
    {
        // format: unlocks                                  lastLevel           highestScores
        // format: 0e0e0.. <e> 0e0e0.. <e> 0e0e0..  <BR> 1              <BR>    0e0e0e0e0...
        if (data != null && data != "")
        {
            string[] splitData = Regex.Split(data, "<BR>");
            string[] splitFirstData = Regex.Split(splitData[0], "<e>");
            for (int i = 0; i < 3; i++)
            {
                string[] splitFirstDataAgain = Regex.Split(splitFirstData[i], "e");
                for (int p = 0; p < 32; p++)
                {
                    unlocks[p, i] = Convert.ToInt32(splitFirstDataAgain[p]);
                }
                splitFirstDataAgain = new string[32];
            }
            lastLevelCleared = Convert.ToInt32(splitData[1]);
            string[] splitThirdData = Regex.Split(splitData[2], "e");
            for (int p = 0; p < 32; p++)
            {
                highestScores[p] = Convert.ToInt32(splitThirdData[p]);
            }
        }
    }
}