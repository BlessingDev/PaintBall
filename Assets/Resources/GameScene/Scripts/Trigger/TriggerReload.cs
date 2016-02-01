using UnityEngine;
using System.Collections;

public class TriggerReload : UITrigger
{
    public int mReload = 0;
    private SpriteRenderer mSprite = null;
    private Sprite mNormalSprite = null;

    void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();
        mNormalSprite = Resources.Load<Sprite>("GameScene\\Sprites\\Blocks\\block_green");
    }

    public override void Trigger()
    {
        GameDirector.Instance.mBulletLimit += mReload;

        mReload = 0;
        mSprite.sprite = mNormalSprite;
    }
}
