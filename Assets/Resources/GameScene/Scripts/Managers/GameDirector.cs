using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDirector : Singletone<GameDirector>
{
    #region Variables
    /// <summary>
    /// 업데이트 할 것인가
    /// </summary>
    public bool mUpdate = true;
    public int mBulletLimit = 10;

    [SerializeField]
    private GameObject mWorld = null;
    private int mDeathCount = 0;
    private string mFileName = "";
    private bool mGameCleared = false;
    #endregion

    #region Capsules
    public int DeathCount
    {
        get
        {
            return mDeathCount;
        }
    }
    public GameObject World
    {
        get
        {
            return mWorld;
        }
    }
    public string FileName
    {
        get
        {
            return mFileName;
        }
    }
    #endregion

    void Start()
    {
        if (FindObjectsOfType<GameDirector>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (mUpdate)
        {
            try
            {
                CameraDirector.Instance.update();
                UIDirector.Instance.update();
                PlayerDirector.Instance.update();
                ShootingDirector.Instance.update();
                MapDirector.Instance.update();

                KeyboardInput();
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("GameManager.Update() " + ex.Message);
            }

        }
        else
        {
            CheckGameStart();
        }
    }

    void LateUpdate()
    {
        if (mUpdate)
        {
            try
            {
                ShootingDirector.Instance.lateUpdate();
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("GameManager.LateUpdate() " + ex.Message);
            }
        }
    }

    #region CustomFunctions
    /// <summary>
    /// 컴퓨터로 시연할 때 사용할 키보드 대체 키 입력
    /// </summary>
    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDirector.Instance.PlayerJump();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerDirector.Instance.PlayerMoveRight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            PlayerDirector.Instance.PlayerMoveRightUp();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerDirector.Instance.PlayerMoveLeft();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            PlayerDirector.Instance.PlayerMoveLeftUp();
        }
    }

    private void CheckGameStart()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("GameScene") && !mGameCleared)
        {
            mUpdate = true;
        }
        else if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("GameScene"))
        {
            mGameCleared = false;
        }
    }

    public void SetStage(int fChapter, int fStage)
    {
        mFileName = fChapter.ToString() + "_" + fStage.ToString();
    }

    public void GameOver()
    {
        Debug.Log("Game Overed");
        mDeathCount++;
        MapDirector.Instance.MoveToSavePoint();
    }

    public void GameClear()
    {
        Debug.Log("Game Cleared");
        mUpdate = false;
        mGameCleared = true;
    }
    #endregion
}
