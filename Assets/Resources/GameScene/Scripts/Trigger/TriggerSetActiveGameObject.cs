using UnityEngine;
using System.Collections;

public class TriggerSetActiveGameObject : UITrigger
{
    [SerializeField]
    private GameObject mObject = null;
    [SerializeField]
    private bool mActive = true;

    public override void Trigger()
    {
        mObject.SetActive(mActive);
    }
}
