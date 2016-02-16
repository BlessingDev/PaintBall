using UnityEngine;
using System.Collections;

public class TriggerPauseGame : UITrigger
{
    public override void Trigger()
    {
        GameDirector.Instance.GamePause();
    }
}
