using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour
{
    private Animator mAnimator = null;

	// Use this for initialization
	void Start ()
    {
        mAnimator = GetComponent<Animator>();
	}
	
    void OnActive()
    {
        if(mAnimator != null)
            mAnimator.Play("Effect");
    }
}
