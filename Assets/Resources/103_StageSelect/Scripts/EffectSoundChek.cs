using UnityEngine;
using System.Collections;

public class EffectSoundChek : MonoBehaviour {

    static bool EffectSoundCheks = true;
    static bool BgSoundChek = true;

    public void EffectChekOn()
    {
        SoundDirector.Instance.EnableEffect = false;
        EffectSoundCheks = false;
    }

    public void EffectChekOff()
    {
        SoundDirector.Instance.EnableEffect = true;
        EffectSoundCheks = true;
    }

    public void BgChekOn()
    {
        SoundDirector.Instance.EnableBGM = false;
        BgSoundChek = false;
    }

    public void BgChekOff()
    {
        SoundDirector.Instance.EnableBGM = true;
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
