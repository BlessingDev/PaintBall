using UnityEngine;
using System.Collections;

public class TriggerMoveTile : UITrigger
{
    private MoveTile mMoveTile = null;

	// Use this for initialization
	void Start ()
    {
        mMoveTile = GetComponent<MoveTile>();
	}

    public override void Trigger()
    {
        mMoveTile.mMove = true;
    }
}
