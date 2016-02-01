using UnityEngine;
using System.Collections;

public class TriggerChangeScene : UITrigger
{
    public int mScene = 0;

    public override void Trigger()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mScene);
    }
}
