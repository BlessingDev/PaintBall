using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour {

    public AudioSource Sound = null;
    public GameObject BgSound = null;

    public void OnSound()
    {
        Sound.volume = 0;
        BgSound.transform.localPosition = new Vector3(1000,1000,1000);
    }
    public void OffSound()
    {
        Sound.volume = 1;
        BgSound.transform.localPosition = new Vector3(90, 35, 0);
    }

}
