using UnityEngine;
using System.Collections;

public class ScoreStar : MonoBehaviour
{
    [SerializeField]
    private Animator[] mStars = new Animator[3];
    [SerializeField]
    private float mStarAnimationTime = 1f;
    private float mTime = 0f;
    private int mCurStar = 0;
	
    void Start()
    {
        mTime = mStarAnimationTime;
    }

    void Update()
    {
        if(mCurStar < GameDirector.Instance.Score)
        {
            mTime += Time.deltaTime;

            if (mTime >= mStarAnimationTime)
            {
                mTime = 0f;
                mStars[mCurStar].gameObject.SetActive(true);
                mStars[mCurStar].SetBool("Effect", true);
                mCurStar++;
            }
        }
    }
}
