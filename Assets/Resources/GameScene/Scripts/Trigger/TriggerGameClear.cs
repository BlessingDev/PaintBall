using UnityEngine;
using System.Collections;

public class TriggerGameClear : UITrigger
{
    public override void Trigger()
    {
        GameDirector.Instance.GameClear();
    }
}
