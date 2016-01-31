using UnityEngine;
using System.Collections;

public class TriggerGameOver : UITrigger
{
    public override void Trigger()
    {
        GameDirector.Instance.GameOver();
    }
}
