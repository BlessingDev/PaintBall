using UnityEngine;
using System.Collections;

public class EffectSound : MonoBehaviour {

    public GameObject EffectSounds = null;
    public AudioSource[] Effects = null;
    public EffectSoundChek ESC = null;
    static bool EffectChek;

    void Update()
    {
        EffectChek = ESC.EffectChekSound;
        if (EffectChek == true)
        {
            EffectSoundOns();
        }
        else EffectSoundOffs();

    }

	public void EffectSoundOns()
    {
        for(int a = 0 ; a <Effects.Length; )
        {
            Effects[a].volume = 1;
            a++;
        }
        if (EffectSounds != null)
        EffectSounds.transform.localPosition = new Vector3(90, -60, 0);
    }

    public void EffectSoundOffs()
    {
        for (int a = 0; a < Effects.Length; )
        {
            Effects[a].volume = 0;
            a++;
        }
        if (EffectSounds != null)
        EffectSounds.transform.localPosition = new Vector3(1000, 1000, 1000);
    }

}
