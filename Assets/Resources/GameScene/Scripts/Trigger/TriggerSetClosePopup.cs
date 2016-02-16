using UnityEngine;
using System.Collections;

public class TriggerSetClosePopup : UITrigger
{
    public override void Trigger()
    {
        UIDirector.Instance.CurPopup.CloseAndInactivate();
    }
}
