using UnityEngine;
using System.Collections;

public class TutorialTile : Tile
{
    /// <summary>
    /// 0은 밟으면 텍스트, 1은 밝혀지면 텍스트
    /// </summary>
    public int mTutorialType = 0;
    /// <summary>
    /// 튜토리얼 내용 인덱스
    /// </summary>
    public int mTutorialText = 0;              
    private BoxCollider2D[] mColliders = null;

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if(mTutorialType == 0)
            {
                TutorialDirector.Instance.HideText();
            }
        }
    }

    public override void OnStepOn()
    {
        if(mTutorialType == 0)
        {
            TutorialDirector.Instance.ShowText(mTutorialText);
        }
    }

    public override void OnUnveil()
    {
        if(mTutorialType == 1)
        {
            TutorialDirector.Instance.ShowText(mTutorialText);
        }
    }

    public override void OnReveil()
    {
        if (mTutorialType == 1)
        {
            TutorialDirector.Instance.HideText();
        }
    }
}
