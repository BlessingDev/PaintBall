using UnityEngine;
using System.Collections;

public class TriggerSetPlayerSpriteShow : UITrigger
{
    private SpriteRenderer mSprite = null;
    private SpriteRenderer mArm = null;

	// Use this for initialization
	void Start ()
    {
        mSprite = PlayerDirector.Instance.Sprite;
        mArm = PlayerDirector.Instance.Arm.GetComponent<SpriteRenderer>();
	}

    public override void Trigger()
    {
        mSprite.enabled = false;
        mArm.enabled = false;
    }
}
