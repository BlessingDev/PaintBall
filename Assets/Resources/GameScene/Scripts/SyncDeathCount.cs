using UnityEngine;
using System.Collections;

public class SyncDeathCount : MonoBehaviour
{
    private UnityEngine.UI.Text mText = null;
	
    void Start()
    {
        mText = GetComponent<UnityEngine.UI.Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        mText.text = GameDirector.Instance.DeathCount.ToString();
	}
}
