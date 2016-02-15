using UnityEngine;
using System.Collections;

public class DesponsableTile : Tile
{
    private float mDestroyTime = 1.5f;
    private float mRespawnTime = 5f;
    private bool mDestroyReserve = false;
    private bool mDestroyed = false;
    private float mTime = 0f;
    private SpriteRenderer mSprite = null;
    private BoxCollider2D[] mColliders = null;

    void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();
        mColliders = GetComponents<BoxCollider2D>();
    }

    public override void update()
    {
        base.update();

        mTime += Time.deltaTime;
        if(mDestroyReserve)
        {
            if(mTime >= mDestroyTime)
            {
                mTime = 0f;
                mDestroyReserve = false;
                mDestroyed = true;
                OnReveil();
                mColliders[0].enabled = false;
                mColliders[1].enabled = false;
            }
        }
        else if(mDestroyed)
        {
            if(mTime >= mRespawnTime)
            {
                mTime = 0f;
                mDestroyed = false;
                mColliders[0].enabled = true;
                mColliders[1].enabled = true;
            }
        }
    }

    public override void OnStepOn()
    {
        mDestroyReserve = true;
        mTime = 0f;

        base.OnStepOn();
    }
}
