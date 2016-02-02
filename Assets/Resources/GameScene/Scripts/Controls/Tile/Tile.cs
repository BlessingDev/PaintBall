using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    #region Variables
    protected float mReveilTime = 1f;
    protected bool mUnveiled = false;
    protected float mTime = 0f;

    [SerializeField]
    private string mTileName = "";
    [SerializeField]
    private UITrigger[] mPlayerStepTrigger = null;
    [SerializeField]
    private UITrigger[] mUnveilTrigger = null;
    [SerializeField]
    private UITrigger[] mReveilTrigger = null;
    private bool mPlayerCollision = false;
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
        mReveilTime = MapDirector.Instance.ReveilTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            mPlayerCollision = true;
            OnStepOn();
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
        if(mUnveiled)
        {
            mTime += Time.deltaTime;
            if(mTime >= mReveilTime)
            {
                OnReveil();
            }
        }
    }
    #endregion

    public virtual void OnStepOn()
    {
        for(int i = 0; i < mPlayerStepTrigger.Length; i++)
        {
            mPlayerStepTrigger[i].Trigger();
        }
    }

    public virtual void OnUnveil()
    {
        mUnveiled = true;
        mTime = 0f;
        Debug.Log(name + "unveil");
        for (int i = 0; i < mUnveilTrigger.Length; i++)
        {
            mUnveilTrigger[i].Trigger();
        }
    }

    public void OnReveil()
    {
        Debug.Log(name + "reveil");
        mUnveiled = false;
        for (int i = 0; i < mReveilTrigger.Length; i++)
        {
            mReveilTrigger[i].Trigger();
        }
    }
}