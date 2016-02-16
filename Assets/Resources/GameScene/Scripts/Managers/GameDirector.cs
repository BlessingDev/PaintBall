using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDirector : Singletone<GameDirector>
{
    #region Variables
    public List<Animator> mAnimators = new List<Animator>();

    [SerializeField]
    private GameObject mWorld = null;
    [SerializeField]
    private bool mUpdate = true;
    private int mDeathCount = 0;
    private string mFileName = "";
    private bool mGameCleared = false;
    private int mScore = 0;
    private Dictionary<string, int> mScoreDic = new Dictionary<string, int>();
    private string mMaxStage = "1_1";
    private bool mInitialized = false;
    private int mBulletLimit = 10;
    private int mMaxBullet = 10;
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
    public float Score
    {
        get
        {
            return mScore;
        }
    }
    public bool IsUpdate
    {
        get
        {
            return mUpdate;
        }
        set
        {
            if (!mGameCleared)
                mUpdate = value;
        }
    }
    public string MaxStage
    {
        get
        {
            return mMaxStage;
        }
    }
    public int BulletLimit
    {
        set
        {
            mMaxBullet = value;
            mBulletLimit = value;
        }

        get
        {
            return mBulletLimit;
        }
    }
    #endregion

    #region VirtualFunctions
    void Start()
    {
        if(!mInitialized)
        {
            mInitialized = true;
            if (FindObjectsOfType<GameDirector>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            mFileName = "1_1";

            if (System.IO.File.Exists(System.Environment.CurrentDirectory + "Assets\\Resources\\Datas\\GameData.data"))
            {
                var reader = FileIODirector.ReadFile(System.Environment.CurrentDirectory + "Assets\\Resources\\Datas\\GameData.data");

                int stageNum = System.Convert.ToInt32(reader.ReadLine());

                for (int i = 0; i < stageNum; i++)
                {
                    int score = System.Convert.ToInt32(reader.ReadLine());
                    mScoreDic.Add((i / 5 + 1).ToString() + "_" + (i % 5 + 1).ToString(), score);
                }

                mMaxStage = ((stageNum + 1) / 5 + 1).ToString() + "_" + ((stageNum + 1) % 5 + 1).ToString();

                mScoreDic.Add(mMaxStage, -1);
            }
            else
            {
                mScoreDic.Add("1_1", -1);
            }
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if(mInitialized)
        {
            switch (level)
            {
                case 2:
                    mAnimators.Clear();
                    mScore = 0;
                    mUpdate = true;
                    //MapDirector가 있다면
                    if (MapDirector.Instance != null)
                        MapDirector.Instance.LoadMap(mFileName);
                    //EffectDirector가 있다면
                    if (EffectDirector.Instance != null)
                        EffectDirector.Instance.StartEffect();
                    break;
                default:
                    mUpdate = false;
                    mGameCleared = false;

                    break;
            }
        }
    }

    void OnDestroy()
    {
        if(mInitialized)
        {
            SaveGameData();
        }
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
    #endregion

    #region CustomFunctions
    private void SaveGameData()
    {
        var writer = FileIODirector.WriteFile("Datas\\GameData.gamedata");

        writer.WriteLine(mScoreDic.Count);
        foreach(var iter in mScoreDic)
        {
            Debug.Log("WriteFile Stage " + iter.Key + " Score " + iter.Value);
            writer.WriteLine(iter.Value);
        }
    }

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
        Debug.Log("Set Stage To " + mFileName);
    }

    public void GameStart()
    {
        mGameCleared = false;
        mUpdate = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
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
        mScore = MapDirector.Instance.GetScore();
        Debug.Log("Score " + mScore);
        int score = 0;
        if(mScoreDic.TryGetValue(mFileName, out score))
        {
            if(score < mScore)
            {
                mScoreDic.Remove(mFileName);
                mScoreDic.Add(mFileName, mScore);
            }
        }
        else
        {
            mScoreDic.Add(mFileName, mScore);
        }

        GamePause();
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
        Debug.Log("Game Resume");
        mUpdate = true;
        SetAnimator(true);
    }

    public void BulletUsed()
    {
        if(mBulletLimit > 0)
            mBulletLimit -= 1;
    }

    public void BulletReload()
    {
        mBulletLimit = mMaxBullet;
    }

    private string GetNextStage(string fStage)
    {
        Debug.Log("FileName" + mFileName);
        int totalStage = ((int)mFileName[0] - 49) * 5;
        totalStage += ((int)mFileName[2] - 48) - 1;

        Debug.Log("totalStage " + totalStage);
        totalStage += 1;
        int chapter = totalStage / 5 + 1;
        int stage = totalStage % 5 + 1;

        return chapter.ToString() + "_" + stage.ToString();
    }

    /// <summary>
    /// 다음 스테이지로 가는 함수
    /// </summary>
    public void NextStage()
    {
        mFileName = GetNextStage(mFileName);

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    #endregion
}
