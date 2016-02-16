using UnityEngine;
using System.Collections;

public class TriggerSetAnimatorSpeed : UITrigger
{
    [SerializeField]
    private Animator mAnimator = null;
    [SerializeField]
    private float mSpeed = 1f;

    void Start()
    {
        if(mAnimator == null)
        {
            mAnimator = GetComponent<Animator>();
        }
    }

    public override void Trigger()
    {
        mAnimator.speed = mSpeed;
    }
}
