using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D mRB = null;
    private SaveTriggeredObjects mTriggers = null;

    public SaveTriggeredObjects TriggerObjs
    {
        get
        {
            return mTriggers;
        }
    }

    public void Start()
    {
        Debug.Log("bullet start");
        mRB = GetComponent<Rigidbody2D>();
        mTriggers = transform.GetChild(0).GetComponent<SaveTriggeredObjects>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Tile"))
        {
            ShootingDirector.Instance.BulletDisable(this);
                MapDirector.Instance.UnveilTiles(mTriggers.TriggerdObjects);
            mTriggers.ClearObjs();
        }
    }

    public void AddForce(Vector2 force)
    {
        mRB.AddForce(force);
    }
}
