using UnityEngine;
using System.Collections;

public class DeathTile : Tile
{
    /// <summary>
    /// 플레이어가 밟았을 경우에 늘어나는 시간
    /// </summary>
    private float mExtraReveilTime = 1f;

    public override void OnStepOn()
    {
        mReveilTime = MapDirector.Instance.ReveilTime + mExtraReveilTime;
        mUnveiled = true;
        mTime = 0f;

        base.OnStepOn();
    }

    public override void OnUnveil()
    {
        mReveilTime = MapDirector.Instance.ReveilTime;

        base.OnUnveil();
    }
}
