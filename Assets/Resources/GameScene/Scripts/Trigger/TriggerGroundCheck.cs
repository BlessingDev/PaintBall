using UnityEngine;
using System.Collections;

public class TriggerGroundCheck : UITrigger
{
    public bool mBool = true;

    public override void Trigger()
    {
        PlayerDirector.Instance.mGrounded = mBool;
    }
}
