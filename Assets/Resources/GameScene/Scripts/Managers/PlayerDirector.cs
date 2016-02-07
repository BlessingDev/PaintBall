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
    private Vector2 mJumpForce = Vector2.zero;
    [SerializeField]
    private float mJumpY = 30f;
    private GameObject mPlayerPrefab = null;
    private GameObject mPlayer = null;
    private GameObject mArm = null;
    private Rigidbody2D mRB = null;
    private SpriteRenderer mSprite = null;
    private Animator mAnimator = null;
    private bool mLeft = false;
    private bool mRight = false;
    private float mAnimationSpeedRate = (1f / 2f);
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
    public SpriteRenderer Sprite
    {
        get
        {
            return mSprite;
        }
    }
    #endregion

    #region VirtualFunctions
    void Start()
    {
        if (mPlayer == null)
        {
            mPlayerPrefab = Resources.Load("GameScene\\Prefabs\\Player") as GameObject;

            mPlayer = Instantiate(mPlayerPrefab) as GameObject;
            mRB = mPlayer.GetComponent<Rigidbody2D>();
            mArm = mPlayer.transform.GetChild(1).gameObject;
            ShootingDirector.Instance.mShootPos = mArm.transform.GetChild(0);
            mSprite = mPlayer.GetComponent<SpriteRenderer>();
            mAnimator = mPlayer.GetComponent<Animator>();

            GameDirector.Instance.mAnimators.Add(mAnimator);
        }
    }

    /// <summary>
    /// GameDirector에서 부를 update
    /// </summary>
    public void update()
    {
        if (mPlayer != null)
        {
            PlayerMove();
            CheckPlayerY();
            PlayerAnimations();
        }
    }
    #endregion

    public void MakePlayer(Vector2 pos)
    {
        if (mPlayer == null)
        {
            Start();
        }

        mPlayer.transform.position = pos;
    }

    private void PlayerMove()
    {
        if (mLeft)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0, 180, 0);

            if (Mathf.Abs(mRB.velocity.x) <= mMaxSpeed && mGrounded)
            {
                mRB.AddForce(new Vector2(-mSpeed, 0));
            }
        }
        if (mRight)
        {
            mPlayer.transform.localEulerAngles = new Vector3(0, 0, 0);

            if (Mathf.Abs(mRB.velocity.x) <= mMaxSpeed && mGrounded)
            {
                mRB.AddForce(new Vector2(mSpeed, 0));
            }
        }
    }

    private void CheckPlayerY()
    {
        if (mPlayer.transform.position.y <= MapDirector.Instance.MapDepth)
        {
            GameDirector.Instance.GameOver();
        }
    }

    private void PlayerAnimations()
    {
        if(mGrounded)
        {
            mAnimator.SetFloat("Speed", Mathf.Abs(mRB.velocity.x));
            mAnimator.speed = 1;
            if (mAnimator.GetFloat("Speed") >= 0.001f)
            {
                mAnimator.speed = Mathf.Abs(mRB.velocity.x) * mAnimationSpeedRate;
            }
        }
        else
        {
            mAnimator.SetFloat("Speed", 0);
        }

        mAnimator.SetBool("Grounded", mGrounded);
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
        if (mGrounded)
        {
            Vector2 jump = mJumpForce;

            if (mLeft)
                jump.x *= -1;
            if (!mLeft && !mRight)
            {
                jump.x = 0;
            }

            Debug.Log("jump");
            mRB.AddForce(jump);
        }
    }
}
