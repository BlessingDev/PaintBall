using UnityEngine;
using System.Collections;

public class ChangScene : MonoBehaviour {

	public void StageSelect()
    {
        Application.LoadLevel(1);
    }
    public void GameScene()
    {
        Application.LoadLevel(2);
    }
    public void StageSelect_1_1()
    {
        //Application.LoadLevel();
    }
}
