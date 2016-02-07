using UnityEngine;
using System.Collections;

public class TriggerOpenPopup : UITrigger
{
    [SerializeField]
    private string mPopupName = "";

    public override void Trigger()
    {
        UIDirector.Instance.OpenPopup(mPopupName);
    }
}
