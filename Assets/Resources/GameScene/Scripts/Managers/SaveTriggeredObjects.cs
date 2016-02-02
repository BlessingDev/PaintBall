using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveTriggeredObjects : MonoBehaviour
{
    [SerializeField]
    private string mTag = "";
    private List<GameObject> mTriggeredObjects = new List<GameObject>();

    public List<GameObject> TriggerdObjects
    {
        get
        {
            return mTriggeredObjects;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals(mTag))
            mTriggeredObjects.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals(mTag))
            mTriggeredObjects.Remove(other.gameObject);
    }

    public void ClearObjs()
    {
        mTriggeredObjects.Clear();
    }
}
