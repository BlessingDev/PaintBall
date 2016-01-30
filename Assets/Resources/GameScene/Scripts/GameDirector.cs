using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDirector : Singletone<GameDirector>
{
    #region Variables
    [SerializeField]
    private GameObject mWorld = null;
    [SerializeField]
    private Stick[] mSticks = null;

    /// <summary>
    /// 업데이트 할 것인가
    /// </summary>
    public bool mUpdate = true;
    #endregion

    public GameObject World
    {
        get
        {
            return mWorld;
        }
    }

    void Update()
    {
        CameraDirector.Instance.update();

        for(int i = 0; i< mSticks.Length; i++)
        {
            mSticks[i].update();
        }
    }
}
