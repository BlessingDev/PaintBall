using UnityEngine;
using System.Collections;

public class TriggerResumeGame : UITrigger
{
    public override void Trigger()
    {
        GameDirector.Instance.GameResume();
    }

    public void ResumeGame()
    {
        Trigger();
    }
}
