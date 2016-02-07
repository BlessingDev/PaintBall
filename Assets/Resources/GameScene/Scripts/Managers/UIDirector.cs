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
    #region Variables
    public bool mUITouched = false;

    [SerializeField]
    private Stick[] mSticks = null;
    [SerializeField]
    private UILayer[] mLayers = null;
    private Dictionary<string, Popup> mPopupDic = new Dictionary<string, Popup>();
    private Popup mCurPopup = null;
    private Canvas mCanvas = null;
    #endregion

    public Vector2 SceneSize
    {
        get
        {
            return new Vector2(mCanvas.pixelRect.width, mCanvas.pixelRect.height);
        }
    }

    // Use this for initialization
    void Start ()
    {
        mCanvas = FindObjectOfType<Canvas>();

        var popups = FindObjectsOfType<Popup>();

        for(int i = 0; i < popups.Length; i++)
        {
            mPopupDic.Add(popups[i].name, popups[i]);
        }
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

    public void OpenPopup(string fPopupName)
    {
        Popup pop = null;
        if(mPopupDic.TryGetValue(fPopupName, out pop))
        {
            mCurPopup = pop;
            pop.OnPopup();
        }
    }

    public void CloseCurPopup()
    {
        if(mCurPopup != null)
        {
            mCurPopup.OnClose();
        }
    }
}
