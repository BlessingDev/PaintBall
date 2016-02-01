using UnityEngine;
using System.Collections;

public class TriggerUnveil : UITrigger
{
    [SerializeField]
    private SpriteRenderer mSprite = null;

    void Start()
    {
        mSprite = GetComponent<SpriteRenderer>();
    }

    public override void Trigger()
    {
        mSprite = GetComponent<SpriteRenderer>();
        mSprite.sortingOrder = 2;
    }
}
