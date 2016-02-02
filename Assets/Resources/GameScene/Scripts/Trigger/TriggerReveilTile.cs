using UnityEngine;
using System.Collections;

public class TriggerReveilTile : UITrigger
{
    private SpriteRenderer mSprite = null;

	// Use this for initialization
	void Start ()
    {
        mSprite = GetComponent<SpriteRenderer>();
	}

    public override void Trigger()
    {
        mSprite.sortingOrder = 0;
    }
}
