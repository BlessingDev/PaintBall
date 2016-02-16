using UnityEngine;
using System.Collections;

public class TriggerSetActiveGameObject : UITrigger
{
    [SerializeField]
    private GameObject[] mObjects = null;
    [SerializeField]
    private bool mActive = true;

    public override void Trigger()
    {
        if(mObjects != null)
        {
            for(int i = 0; i < mObjects.Length; i++)
            {
                mObjects[i].SetActive(mActive);
            }
        }
    }

    public void SetActiveGameObject()
    {
        Trigger();
    }
}
