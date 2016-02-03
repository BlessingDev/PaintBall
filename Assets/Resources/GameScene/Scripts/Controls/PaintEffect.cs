using UnityEngine;
using System.Collections;

public class PaintEffect : Effect
{
    public Bullet mBullet = null;

    public void Unveil()
    {
        MapDirector.Instance.UnveilTiles(mBullet.TriggerObjs.TriggerdObjects);
    }
}
