using UnityEngine;
using System.Collections;

public class PlayerDirector : Singletone<PlayerDirector>
{
    #region Variables
    public bool mGrounded = true;

    [SerializeField]
    private Stick mStick = null;
    [SerializeField]
    private float mMaxSpeed = 10;
    [SerializeField]
    private float mSpeed = 10f;
    [SerializeField]
    private float mJumpForce = 10f;
    [SerializeField]
    private float mJumpY = 30f;
    private GameObject mPlayerPrefab = null;
    private GameObject mPlayer = null;
    private GameObject mArm = null;
    private Rigidbody2D mRB = null;
    private bool mLeft = false;
    private bool mRight = false;
    #endregion

    #region Capsules
    public GameObject Player
    {
        get
        {
            return mPlayer;
        }
    }
    public GameObject Arm
    {
        get
        {
            return mArm;
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
        mArm = mPlayer.transform.GetChild(1).gameObject;
        ShootingDirector.Instance.mShootPos = mArm.transform.GetChild(0);
    }

    private void PlayerMove()
    {
        if(mLeft)
        {
            if (Mathf.Abs(mRB.velocity.x) <= mMaxSpeed && mGrounded)
            {
                mRB.AddForce(new Vector2(-mSpeed, 0));
            }
        }
        if(mRight)
        {
            if (Mathf.Abs(mRB.velocity.x) <= mMaxSpeed && mGrounded)
            {
                mRB.AddForce(new Vector2(mSpeed, 0));
            }
        }
    }

    public void PlayerMoveRight()
    {
        mRight = true;
    }

    public void PlayerMoveRightUp()
    {
        mRight = false;
    }

    public void PlayerMoveLeft()
    {
        mLeft = true;
    }

    public void PlayerMoveLeftUp()
    {
        mLeft = false;
    }

    /// <summary>
    /// 외부 버튼에서 부를 함수
    /// </summary>
    public void PlayerJump()
    {
        if(mGrounded)
        {
            Debug.Log("jump");
            mRB.AddForce(new Vector2(0, mJumpForce));
        }
    }
}
