using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour {

    public AudioSource[] Sound = null;
    public GameObject BgSound = null;
    public EffectSoundChek ESC = null;
    static bool BgSoundChek;

    void Update()
    {
        BgSoundChek = ESC.BgChekSound;
        if (BgSoundChek == true)
        {
            OnSound();
        }
        else OffSound();

    }

    public void OnSound()
    {
        for (int a = 0; a < Sound.Length; )
        {
            Sound[a].volume = 1;
            a++;
        }
        if(BgSound != null)
        BgSound.transform.localPosition = new Vector3(90, 35, 0);
    }
    public void OffSound()
    {
        for (int a = 0; a < Sound.Length; )
        {
            Sound[a].volume = 0;
            a++;
        }
        if (BgSound != null)
        BgSound.transform.localPosition = new Vector3(1000,1000,1000);
    }

}
