using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private string mTileName = "";
    [SerializeField]
    private UITrigger[] mPlayerStepTrigger = null;
    [SerializeField]
    private UITrigger[] mUnveilTrigger = null;
    private bool mPlayerCollision = false;

    public bool PlayerCollision
    {
        get
        {
            return mPlayerCollision;
        }
    }

    #region VirtualFunctions
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

    }
    #endregion

    public void OnStepOn()
    {
        for(int i = 0; i < mPlayerStepTrigger.Length; i++)
        {
            mPlayerStepTrigger[i].Trigger();
        }
    }

    public void OnUnveil()
    {
        Debug.Log(name + "unveil");
        for (int i = 0; i < mUnveilTrigger.Length; i++)
        {
            mUnveilTrigger[i].Trigger();
        }
    }
}