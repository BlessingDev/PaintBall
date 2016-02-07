using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private int mUILayerIndex = 0;
    [SerializeField]
    private UITrigger[] mEnterTriggers = null;
    [SerializeField]
    private UITrigger[] mExitTriggers = null;
    private Vector2 mInvisiblePos = new Vector2(1000, 1000);
	
    public void OnPopup()
    {
        for(int i = 0; i < mEnterTriggers.Length; i++)
        {
            mEnterTriggers[i].Trigger();
        }

        transform.position = UIDirector.Instance.SceneSize / 2;
        UIDirector.Instance.SetLayerEnableTouch(0, false);
        UIDirector.Instance.SetLayerEnableTouch(mUILayerIndex, true);
        GameDirector.Instance.GamePause();
    }

    public void OnClose()
    {
        for (int i = 0; i < mExitTriggers.Length; i++)
        {
            mExitTriggers[i].Trigger();
        }

        transform.position = mInvisiblePos;
        UIDirector.Instance.SetLayerEnableTouch(0, true);
        UIDirector.Instance.SetLayerEnableTouch(mUILayerIndex, false);
        GameDirector.Instance.GameResume();
    }
}
