using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class UILayer
{
    public UnityEngine.UI.Image[] mImages;
}

public class UIDirector : Singletone<UIDirector>
{
    [SerializeField]
    private Stick[] mSticks = null;
    [SerializeField]
    private UILayer[] mLayers = null;

	// Use this for initialization
	void Start ()
    {
	
	}
	
    public void update()
    {
        for(int i = 0; i < mSticks.Length; i++)
        {
            mSticks[i].update();
        }
    }

    public void SetLayerEnableTouch(int fLayer, bool fCheck)
    {
        if(fLayer >= 0 && fLayer < mLayers.Length)
        {
            for(int i = 0; i < mLayers[fLayer].mImages.Length; i++)
            {
                mLayers[fLayer].mImages[i].raycastTarget = fCheck;
            }
        }
    }
}
