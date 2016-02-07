using UnityEngine;
using System.Collections;

public class TriggerCloseCurPopup : UITrigger
{
    public override void Trigger()
    {
        UIDirector.Instance.CloseCurPopup();
    }
}
