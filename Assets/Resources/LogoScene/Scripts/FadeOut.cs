using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    
    public UnityEngine.UI.Image Sprite1;
    Color color1;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        Sprite1.color = new Vector4(255.0f, 255.0f, 255.0f, Sprite1.color.a - 0.01f);
	}
}
