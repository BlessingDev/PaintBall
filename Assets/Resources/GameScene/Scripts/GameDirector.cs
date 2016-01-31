using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDirector : Singletone<GameDirector>
{
    #region Variables
    [SerializeField]
    private GameObject mWorld = null;

    /// <summary>
    /// 업데이트 할 것인가
    /// </summary>
    public bool mUpdate = true;
    #endregion

    public GameObject World
    {
        get
        {
            return mWorld;
        }
    }

    void Update()
    {
        if(mUpdate)
        {
            CameraDirector.Instance.update();
            UIDirector.Instance.update();
            PlayerDirector.Instance.update();
            ShootingDirector.Instance.update();

            KeyboardInput();
        }
    }

    /// <summary>
    /// 컴퓨터로 시연할 때 사용할 키보드 대체 키 입력
    /// </summary>
    private void KeyboardInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDirector.Instance.PlayerJump();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerDirector.Instance.PlayerMoveRight();
        }
        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            PlayerDirector.Instance.PlayerMoveRightUp();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerDirector.Instance.PlayerMoveLeft();
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            PlayerDirector.Instance.PlayerMoveLeftUp();
        }
    }
}
