using UnityEngine;
using System.Collections;

public class TriggerReveilTile : UITrigger
{
    private SpriteRenderer mSprite = null;
    private Animator mAnimator = null;

	// Use this for initialization
	void Start ()
    {
        mSprite = GetComponent<SpriteRenderer>();
        mAnimator = GetComponent<Animator>();
	}

    public override void Trigger()
    {
        mAnimator.SetBool("Unveil", false);
    }

    public void Veiled()
    {
        Debug.Log(name + "Veiled");
        mSprite.sortingOrder = 0;
    }
}
