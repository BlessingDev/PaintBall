using UnityEngine;
using System.Collections;

public class TriggerCheckPoint : UITrigger
{
    public override void Trigger()
    {
        MapDirector.Instance.CheckPoint = PlayerDirector.Instance.Player.transform.position;
    }
}
