using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CollisionTriggers
{
    public string mCollisionTag = "";
    public UITrigger[] mEnterTriggers;
    public UITrigger[] mStayTriggers;
    public UITrigger[] mExitTriggers;
}

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField]
    private CollisionTriggers[] mTriggers = null;
    private Dictionary<string, UITrigger[]> mEnterTriggers = new Dictionary<string, UITrigger[]>();
    private Dictionary<string, UITrigger[]> mExitTriggers = new Dictionary<string, UITrigger[]>();
    private Dictionary<string, UITrigger[]> mStayTriggers = new Dictionary<string, UITrigger[]>();

	// Use this for initialization
	void Start ()
    {
	    for(int i = 0; i < mTriggers.Length; i ++)
        {
            mEnterTriggers.Add(mTriggers[i].mCollisionTag, mTriggers[i].mEnterTriggers);
            mExitTriggers.Add(mTriggers[i].mCollisionTag, mTriggers[i].mExitTriggers);
            mStayTriggers.Add(mTriggers[i].mCollisionTag, mTriggers[i].mStayTriggers);
        }
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        UITrigger[] triggers = null;

        if(mEnterTriggers.TryGetValue(other.gameObject.tag, out triggers))
        {
            for(int i = 0; i < triggers.Length; i++)
            {
                triggers[i].Trigger();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        UITrigger[] triggers = null;

        if (mStayTriggers.TryGetValue(other.gameObject.tag, out triggers))
        {
            for (int i = 0; i < triggers.Length; i++)
            {
                triggers[i].Trigger();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(name + " TriggerExit " + other.tag);
        UITrigger[] triggers = null;

        if (mExitTriggers.TryGetValue(other.gameObject.tag, out triggers))
        {
            for (int i = 0; i < triggers.Length; i++)
            {
                triggers[i].Trigger();
            }
        }
    }
}
