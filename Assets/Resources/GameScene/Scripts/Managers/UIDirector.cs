﻿using UnityEngine;
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
    private UnityEngine.UI.Text mDeathCount = null;
    [SerializeField]
    private UnityEngine.UI.Text mBulletCount = null;
    [SerializeField]
    private UILayer[] mLayers = null;
    private Dictionary<string, Popup> mPopupDic = new Dictionary<string, Popup>();
    private Popup mCurPopup = null;
    private Canvas mCanvas = null;
    #endregion

    #region Capsules
    public Vector2 SceneSize
    {
        get
        {
            return new Vector2(mCanvas.pixelRect.width, mCanvas.pixelRect.height);
        }
    }
    public Popup CurPopup
    {
        get
        {
            return mCurPopup;
        }
    }
    #endregion

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
        if(mDeathCount != null)
        {
            mDeathCount.text = GameDirector.Instance.DeathCount.ToString();
        }
        if(mBulletCount != null)
        {
            mBulletCount.text = GameDirector.Instance.BulletLimit.ToString();
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