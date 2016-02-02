using UnityEngine;
using System.Collections;

public class TriggerUnveil : UITrigger
{
    [SerializeField]
    private SpriteRenderer mSprite = null;
    private Animator mAnimator = null;

    void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();
        mAnimator = GetComponent<Animator>();
    }

    public override void Trigger()
    {
        mAnimator.SetBool("Unveil", true);
        mSprite.sortingOrder = 3;
    }
}
