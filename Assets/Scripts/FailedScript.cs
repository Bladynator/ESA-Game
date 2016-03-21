using UnityEngine;
using System.Collections;

public class FailedScript : MonoBehaviour
{
    [SerializeField]
    Texture2D text;
    Vector2 textSize;
    GameController gameController;
    int stage = 0;
    int timer = 50;
    float textPosY = Screen.height / 1.3f;

    void Start()
    {
        textSize = new Vector2(Screen.width / 4f, Screen.height / 5);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        switch (stage)
        {
            case 0:
                {
                    textPosY -= 1.7f;
                    if(textPosY < 174)
                    {
                        stage = 1;
                    }
                    break;
                }
            case 1:
                {
                    gameController.doneWithAnimation = true;
                    break;
                }
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 2 - (textSize.x / 2), Screen.height / 2 - (textSize.y / 2) - textPosY, textSize.x, textSize.y), text);
    }
}
