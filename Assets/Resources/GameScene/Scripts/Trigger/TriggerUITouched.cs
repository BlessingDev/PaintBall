using UnityEngine;
using System.Collections;

public class TriggerUITouched : UITrigger
{
    public bool mBool = true;

    public override void Trigger()
    {
        UIDirector.Instance.mUITouched = mBool;
    }
}
