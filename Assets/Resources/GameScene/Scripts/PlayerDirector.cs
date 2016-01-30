using UnityEngine;
using System.Collections;

public class PlayerDirector : Singletone<PlayerDirector>
{
    #region Variables
    [SerializeField]
    private Stick mStick = null;
    [SerializeField]
    private float mMaxSpeed = 10;
    private GameObject mPlayerPrefab = null;
    private GameObject mPlayer = null;
    private Rigidbody2D mRB = null;
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
        if(mPlayer != null)
        {
            PlayerMove();
        }
    }
    #endregion

    public void MakePlayer(Vector2 pos)
    {
        if (mPlayerPrefab == null)
            Start();

        mPlayer = Instantiate(mPlayerPrefab) as GameObject;
        mPlayer.transform.position = pos;

        mRB = mPlayer.GetComponent<Rigidbody2D>();
    }

    private void PlayerMove()
    {
        if(Mathf.Abs(mRB.velocity.x) <= mMaxSpeed)
        {
            mRB.AddForce(new Vector2(mStick.StickVector.x, 0));
        }
    }
}
