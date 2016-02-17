using UnityEngine;
using System.Collections;

public class EffectSoundChek : MonoBehaviour {

    static bool EffectSoundCheks = true;
    static bool BgSoundChek = true;

    public void EffectChekOn()
    {
        EffectSoundCheks = false;
    }

    public void EffectChekOff()
    {
        EffectSoundCheks = true;
    }

    public void BgChekOn()
    {
        BgSoundChek = false;
    }

    public void BgChekOff()
    {
        BgSoundChek = true;
    }

    public bool EffectChekSound
    {
        get
        {
            return EffectSoundCheks;
        }
    }

    public bool BgChekSound
    {
        get
        {
            return BgSoundChek;
        }
    }
}
