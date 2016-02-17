using UnityEngine;
using System.Collections;

public class TileEffectAnimation : MonoBehaviour
{
    private SpriteRenderer mRenderer = null;
    private Animator mAnimator = null;

    [HideInInspector]
    public UITrigger[] mTriggers = null;
    [HideInInspector]
    public bool mPausedOnStart = false;

    void Start()
    {
        mRenderer = GetComponent<SpriteRenderer>();
        mAnimator = GetComponent<Animator>();

        GameDirector.Instance.mAnimators.Add(mAnimator);
    }

    public void OnStepOn()
    {
        mRenderer.enabled = true;
        mAnimator.SetBool("Effect", true);
        
        if (mPausedOnStart)
        {
            PlayerDirector.Instance.Init();
            GameDirector.Instance.IsUpdate = false;
        }
    }

    void EffectEnd()
    {
        for(int i = 0; i < mTriggers.Length; i++)
        {
            mTriggers[i].Trigger();
        }
        if (mPausedOnStart)
            GameDirector.Instance.IsUpdate = true;

        mAnimator.SetBool("Effect", false);
        mRenderer.enabled = false;
    }
}
