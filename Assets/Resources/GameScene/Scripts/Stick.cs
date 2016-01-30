using UnityEngine;
using System.Collections;

public class Stick : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject mStick = null;
    [SerializeField]
    private float mRadius = 30;
    [SerializeField]
    private float mRate = 0.01f;
    private bool mTouching = false;
    #endregion

    public Vector2 StickVector
    {
        get
        {
            return mStick.transform.localPosition;
        }
    }
	
    public void update()
    {
        StickMove();
    }

    #region CustomFunctions
    private void StickMove()
    {
        if(mTouching)
        {
            Vector2 mouseLocal = transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            float dis = Vector2.Distance(Vector2.zero, mouseLocal);

            if(dis <= mRadius)
            {
                mStick.transform.localPosition = mouseLocal;
            }
            else
            {
                Vector2 pos = mouseLocal.normalized * mRadius;

                mStick.transform.localPosition = pos;
            }
        }
        else
        {
            mStick.transform.localPosition = Vector2.zero;
        }
    }

    public void OnTouch()
    {
        Debug.Log("OnTouch");
        mTouching = true;
    }

    public void OnRelease()
    {
        mTouching = false;
    }
    #endregion
}
