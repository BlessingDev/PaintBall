using UnityEngine;
using System.Collections;

public class MoveTile : Tile
{
    public float mStopTime = 0f;
    public Vector2 mStartPos = Vector2.zero;
    public Vector2 mEndPos = Vector2.zero;
    public float mMoveTime = 1f;
    /// <summary>
    /// 움직이고 있다면 true, 움직이지 않는다면 false
    /// </summary>
    public bool mMove = false;

    private float mTime = 0f;
    private bool mMoving = false;
    private bool mStart = true;

    /// <summary>
    /// Director에 의해 불려질 update 함수
    /// </summary>
    public override void update()
    {
        base.update();

        if(mMove)
        {
            DelayAndMove();
        }
    }

    private void DelayAndMove()
    {
        mTime += Time.deltaTime;
        if(!mMoving)
        {
            if(mTime >= mStopTime)
            {
                mTime = 0f;
                mMoving = true;
            }
        }
        else
        {
            if(mStart)
            {
                float rate = mTime / mMoveTime;
                transform.position = Vector2.Lerp(mStartPos, mEndPos, rate);

                if(rate >= 1)
                {
                    transform.position = Vector2.Lerp(mStartPos, mEndPos, 1);
                    mMoving = false;
                    mTime = 0f;
                    mStart = false;
                }
            }
            else
            {
                float rate = mTime / mMoveTime;
                transform.position = Vector2.Lerp(mEndPos, mStartPos, rate);

                if (rate >= 1)
                {
                    transform.position = Vector2.Lerp(mEndPos, mStartPos, 1);
                    mMoving = false;
                    mTime = 0f;
                    mStart = true;
                }
            }
        }
    }
}
