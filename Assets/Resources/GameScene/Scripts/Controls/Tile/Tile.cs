using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    #region Variables
    protected float mReveilTime = 1f;
    protected bool mUnveiled = false;
    protected float mTime = 0f;
    public bool mVeilOn = true;
    public bool mReveilOn = true;

    [SerializeField]
    private string mTileName = "";
    [SerializeField]
    private UITrigger[] mPlayerStepTrigger = null;
    [SerializeField]
    private UITrigger[] mEffectAnimationEndTrigger = null;
    [SerializeField]
    private UITrigger[] mUnveilTrigger = null;
    [SerializeField]
    private UITrigger[] mReveilTrigger = null;
    [SerializeField]
    private TileEffectAnimation mTileAnimation = null;
    [SerializeField]
    private bool mPauseOnEvent = false;
    [SerializeField]
    private bool mOneShotEffect = false;
    private bool mPlayerCollision = false;
    /// <summary>
    /// 해당 타일 위에 올라선 것으로 인정할 좌표 차이
    /// </summary>
    private float mStepOnError = 0.6f;
    #endregion

    #region Capsules
    public bool PlayerCollision
    {
        get
        {
            return mPlayerCollision;
        }
    }
    #endregion

    #region VirtualFunctions
    void Start()
    {
        Animator ani = GetComponent<Animator>();

        if (ani != null)
            GameDirector.Instance.mAnimators.Add(ani);

        mReveilTime = MapDirector.Instance.ReveilTime;
        if (mTileAnimation != null)
        {
            mTileAnimation.mTriggers = mEffectAnimationEndTrigger;
            mTileAnimation.mPausedOnStart = mPauseOnEvent;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            mPlayerCollision = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            mPlayerCollision = false;
        }
    }

    public virtual void update()
    {
        if (mUnveiled)
        {
            mTime += Time.deltaTime;
            if (mTime >= mReveilTime)
            {
                OnReveil();
            }
        }

        CheckStepOn();
    }
    #endregion

    #region CustomFunctions
    private void CheckStepOn()
    {
        if (mPlayerCollision)
        {
            if (Mathf.Abs(transform.position.x - PlayerDirector.Instance.Player.transform.position.x) <= mStepOnError)
            {
                OnStepOn();
                mPlayerCollision = false;
            }
        }
    }

    public virtual void OnStepOn()
    {
        Debug.Log("StepOn " + name);
        if (mTileAnimation != null)
        {
            mTileAnimation.OnStepOn();

            if (mOneShotEffect)
                mTileAnimation = null;
        }
        else
        {
            Debug.Log(name + " TileAnimation is null");
        }

        for (int i = 0; i < mPlayerStepTrigger.Length; i++)
        {
            mPlayerStepTrigger[i].Trigger();
        }
    }

    public virtual void OnUnveil()
    {
        if (mVeilOn)
        {
            mUnveiled = true;
            mTime = 0f;
            Debug.Log(name + "unveil");
            for (int i = 0; i < mUnveilTrigger.Length; i++)
            {
                mUnveilTrigger[i].Trigger();
            }
        }
    }

    public virtual void OnReveil()
    {
        if (mReveilOn)
        {
            Debug.Log(name + "reveil");
            mUnveiled = false;
            for (int i = 0; i < mReveilTrigger.Length; i++)
            {
                mReveilTrigger[i].Trigger();
            }
        }
    }
    #endregion
}