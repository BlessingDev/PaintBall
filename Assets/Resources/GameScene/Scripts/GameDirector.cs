using UnityEngine;
using System.Collections;

public class GameDirector : Singletone<GameDirector>
{
    #region Variables
    [SerializeField]
    private GameObject mWorld = null;

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
    }
}
