using UnityEngine;
using System.Collections;

public class TriggerNextStage : UITrigger
{
    public override void Trigger()
    {
        GameDirector.Instance.NextStage();
    }
}
