using UnityEngine;
using System.Collections;

public class PlayerDirector : Singletone<PlayerDirector>
{
    #region Variables
    private GameObject mPlayerPrefab = null;
    private GameObject mPlayer = null;
    #endregion

    #region Capsules
    public GameObject Player
    {
        get
        {
            return mPlayer;
        }
    }
    #endregion

    #region VirtualFunctions
    void Start()
    {
        mPlayerPrefab = Resources.Load("GameScene\\Prefabs\\Player") as GameObject;
    }

    /// <summary>
    /// GameDirector에서 부를 update
    /// </summary>
    public void update()
    {

    }
    #endregion

    public void MakePlayer(Vector2 pos)
    {
        if (mPlayerPrefab == null)
            Start();

        mPlayer = Instantiate(mPlayerPrefab) as GameObject;
        mPlayer.transform.position = pos;
    }
}
