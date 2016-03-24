using UnityEngine;
using System.Collections;

public class ControllerLevel3 : MiniGameMainController
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void Done()
    {
        WinState(true, timer * 7, "BlueHuman");
    }
}
