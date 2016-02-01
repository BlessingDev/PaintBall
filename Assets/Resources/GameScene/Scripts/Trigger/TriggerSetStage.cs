using UnityEngine;
using System.Collections;

public class TriggerSetStage : UITrigger
{
    public int mChpater = 1;
    public int mStage = 1;

    public override void Trigger()
    {
        GameDirector.Instance.SetStage(mChpater, mStage);
    }
}
