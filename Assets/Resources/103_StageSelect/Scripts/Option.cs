using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour {

    public AudioSource Sound = null;

    public void OnSound()
    {
        Sound.volume = 1;
    }
    public void OffSound()
    {
        Sound.volume = 0;
    }

}
