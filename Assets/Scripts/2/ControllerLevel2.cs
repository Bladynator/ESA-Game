using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerLevel2 : MiniGameMainController
{
    public float currentStatus = 0;
    [SerializeField]
    Texture2D emptyBar, fullBar;
    [SerializeField]
    float reduceValue;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        if (!end)
        {
            if (currentStatus > 0)
            {
                currentStatus -= reduceValue;
            }
        }
    }

    public override void OnGUI()
    {
        base.OnGUI();
        GUI.DrawTexture(new Rect(Screen.width / 2 - 77, Screen.height / 5 - 2, 150, 29), emptyBar);
        if (!end)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - 77, Screen.height / 5 - 2, currentStatus, 29), fullBar);
        }

        if(currentStatus >= 150 && !end)
        {
            WinState(true, timer * 7, " ");
        }
    }
}
