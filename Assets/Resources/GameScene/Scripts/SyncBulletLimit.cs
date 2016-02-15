using UnityEngine;
using System.Collections;

public class SyncBulletLimit : MonoBehaviour
{
    private UnityEngine.UI.Text mText;

	// Use this for initialization
	void Start ()
    {
        mText = GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        mText.text = GameDirector.Instance.BulletLimit.ToString();
	}
}
