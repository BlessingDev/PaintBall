using UnityEngine;
using System.Collections;

public class AnimationEnd : MonoBehaviour {

    public bool Chek = false;
    public Butten but = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

    public void ChekTrue()
    {
        Chek = true;
    }

	void Update () {
        if (Chek == true)
        {
            but.AnimationChek();
        }
	
	}
}
