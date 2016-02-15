using UnityEngine;
using System.Collections;

public class TriggerDoubleJump : UITrigger
{
    public override void Trigger()
    {
        PlayerDirector.Instance.PlayerDoubleJump();
    }
}
