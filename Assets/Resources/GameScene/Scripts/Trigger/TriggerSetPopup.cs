using UnityEngine;
using System.Collections;

public class TriggerSetPopup : UITrigger
{
    public override void Trigger()
    {
        UIDirector.Instance.CurPopup.OpenAndActivate();
    }
}
