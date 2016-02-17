using UnityEngine;
using System.Collections;

public class ChangScene : MonoBehaviour {

    public void MainScene()
    {
        Application.LoadLevel(0);
    }
	public void StageSelect()
    {
        Application.LoadLevel(1);
    }
    public void GameScene()
    {
        Application.LoadLevel(2);
    }
    public void CreditScene()
    {
        Application.LoadLevel(3);
    }
    public void StageSelect_1_1()
    {
        //Application.LoadLevel();
    }
}
