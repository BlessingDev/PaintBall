using UnityEngine;
using System.Collections;

public class TriggerSetBoolAnimator : UITrigger
{
    [SerializeField]
    private string mParameter = "";
    [SerializeField]
    private bool mValue = false;
    [SerializeField]
    private Animator mAnimator = null;

    void Start()
    {
        if (mAnimator == null)
            mAnimator = GetComponent<Animator>();
    }

    public override void Trigger()
    {
        mAnimator.SetBool(mParameter, mValue);
    }

    public void Act()
    {
        Trigger();
    }
}
