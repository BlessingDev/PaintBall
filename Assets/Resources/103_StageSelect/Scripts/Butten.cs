using UnityEngine;
using System.Collections;
using UnityEngine.UI;//이녀석이 있어야 Image를 사용가능

public class Butten : MonoBehaviour {

    //public GameObject[] butten = null;
    //public Image[] image = null;
    //public float Speed = 10;
    //public float Mine = 100;
    //float[] MPos_y;
    public Animator[] Ani = null;


    public void AnimationChek()
    {
        for(int i = 0; i<Ani.Length;)
        {
            Ani[i].enabled = true;
            i++;
        }
    }
	
	
}
