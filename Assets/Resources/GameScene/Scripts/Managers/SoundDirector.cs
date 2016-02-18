using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundDirector : Singletone<SoundDirector>
{
    private bool mEnableEffect = true;
    private bool mEnableBGM = true;
    private Dictionary<string, AudioSource> mBGMDatas = new Dictionary<string, AudioSource>();
    private AudioSource mCurBGM = null;

    #region Capsules
    public AudioSource CurBGM
    {
        get
        {
            return mCurBGM;
        }
    }
    public bool EnableEffect
    {
        set
        {
            mEnableEffect = value;

            if(!mEnableEffect)
            {
                StopAllEffects();
            }
        }
        get
        {
            return mEnableEffect;
        }
    }
    public bool EnableBGM
    {
        set
        {
            mEnableBGM = value;

            if (!mEnableBGM)
                StopCurBGM();
        }
        get
        {
            return mEnableBGM;
        }
    }
    #endregion

    #region VirtualFunctions
    void Start()
    {
        if(FindObjectsOfType<SoundDirector>().Length >= 2)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        var audioSources = Resources.LoadAll<AudioSource>(System.Environment.CurrentDirectory + "\\Assets\\Resources\\GameScene\\Sounds\\BGMs\\");
        
        for(int i = 0; i < audioSources.Length; i++)
        {
            mBGMDatas.Add(audioSources[i].name, audioSources[i]);
        } 
    }

    void Update()
    {
        if(mCurBGM != null)
            mCurBGM.transform.position = Camera.main.transform.position;
    }
    #endregion

    #region CustomFunctions
    public bool PlayBGM(string fSoundName, bool fNotDestroyOnLoad)
    {
        Debug.Log("Try Play " + fSoundName);
        if(mCurBGM == null && mEnableBGM)
        {
            if (mBGMDatas.ContainsKey(fSoundName))
            {
                AudioSource sound = null;
                if (mBGMDatas.TryGetValue(fSoundName, out sound))
                {
                    AudioSource audio = Instantiate(sound);
                    audio.transform.position = Camera.main.transform.position;
                    mCurBGM = audio;

                    if (fNotDestroyOnLoad)
                        DontDestroyOnLoad(audio);
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StopCurBGM()
    {
        if(mCurBGM != null)
        {
            Destroy(mCurBGM);
            mCurBGM = null;
        }
    }

    public void StopAllEffects()
    {

    }
    #endregion
}
