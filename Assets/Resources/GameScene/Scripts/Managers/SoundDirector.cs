using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundDirector : Singletone<SoundDirector>
{
    private Dictionary<string, AudioSource> mSoundDatas = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> mPlayingSounds = new Dictionary<string, AudioSource>();

    void Start()
    {
        var audioSources = Resources.LoadAll<AudioSource>(System.Environment.CurrentDirectory + "\\Assets\\Resources\\GameScene\\Sounds\\");
        
        for(int i = 0; i < audioSources.Length; i++)
        {
            mSoundDatas.Add(audioSources[i].name, audioSources[i]);
        } 
    }

    public void PlaySound(string fSoundName)
    {
        if(!mPlayingSounds.ContainsKey(fSoundName))
        {
            if (mSoundDatas.ContainsKey(fSoundName))
            {
                AudioSource sound = null;
                if (mSoundDatas.TryGetValue(fSoundName, out sound))
                {
                    AudioSource audio = Instantiate(sound);
                    audio.transform.parent = Camera.main.transform;
                    mPlayingSounds.Add(fSoundName, audio);
                }
            }
        }
    }

    public void StopSound(string fSoundName)
    {
        if(mPlayingSounds.ContainsKey(fSoundName))
        {
            AudioSource audio = null;
            if(mPlayingSounds.TryGetValue(fSoundName, out audio))
            {
                Destroy(audio);
            }
            mPlayingSounds.Remove(fSoundName);
        }
    }
}
