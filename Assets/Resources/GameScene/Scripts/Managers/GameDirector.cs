using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDirector : Singletone<GameDirector>
{
    #region Variables
    /// <summary>
    /// 업데이트 할 것인가
    /// </summary>
    public int mBulletLimit = 10;
    public List<Animator> mAnimators = new List<Animator>();
    public bool mUpdate = true;

    [SerializeField]
    private GameObject mWorld = null;
    private int mDeathCount = 0;
    private string mFileName = "";
    private bool mGameCleared = false;
    private int mScore = 0;
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

        mFileName = "1_1";

        mUpdate = true;
        //MapDirector가 있다면
        if(MapDirector.Instance != null)
            MapDirector.Instance.LoadMap(mFileName);

        //EffectDirector가 있다면
        if(EffectDirector.Instance != null)
            EffectDirector.Instance.StartEffect();
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
                EffectDirector.Instance.update();

                KeyboardInput();
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("GameManager.Update() " + ex.Message);
            }

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

    private void SetAnimator(bool able)
    {
        foreach(var iter in mAnimators)
        {
            iter.enabled = able;
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
        Debug.Log("Game Cleared ");
        mScore = MapDirector.Instance.GetScore();
        mUpdate = false;
        mGameCleared = true;
        mDeathCount = 0;
    }

    public void GamePause()
    {
        mUpdate = false;
        SetAnimator(false);
    }

    public void GameResume()
    {
        mUpdate = true;
        SetAnimator(true);
    }
    #endregion
}
