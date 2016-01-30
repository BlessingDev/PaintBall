using UnityEngine;
using System.Collections;

public class UITrigger : MonoBehaviour
{
    public virtual void Trigger()
    {
        Debug.Log("UITrigger.Trigger() Called");
    }
}
