using UnityEngine;
using System.Collections;

public class TriggerStageOpen : MonoBehaviour
{
    [SerializeField]
    private int mChapter = 1;
    [SerializeField]
    private int mStage = 1;

	// Use this for initialization
	void Start ()
    {
	    if(GameDirector.Instance.IsOpend(mChapter, mStage))
        {
            gameObject.SetActive(false);
        }
	}
}
