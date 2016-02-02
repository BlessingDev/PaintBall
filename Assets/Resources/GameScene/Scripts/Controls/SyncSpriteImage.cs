using UnityEngine;
using System.Collections;

public class SyncSpriteImage : MonoBehaviour
{
    private UnityEngine.UI.Image mImage = null;
    private SpriteRenderer mSprite = null;

	// Use this for initialization
	void Start ()
    {
        mImage = GetComponent<UnityEngine.UI.Image>();
        mSprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        mImage.sprite = mSprite.sprite;
	}
}
