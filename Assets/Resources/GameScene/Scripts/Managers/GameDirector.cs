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
        if (FindObjectsOfType<GameDirector>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        if (!mInitialized)
        {
            mInitialized = true;

            DontDestroyOnLoad(gameObject);

            mFileName = "1_1";

            if (System.IO.File.Exists(System.Environment.CurrentDirectory + "\\Assets\\Resources\\Datas\\GameData.gamedata"))
            {
                var reader = FileIODirector.ReadFile("Datas\\GameData.gamedata");

                int stageNum = System.Convert.ToInt32(reader.ReadLine());

                for (int i = 0; i < stageNum; i++)
                {
                    int score = System.Convert.ToInt32(reader.ReadLine());
                    mScoreDic.Add((i / 5 + 1).ToString() + "_" + (i % 5 + 1).ToString(), score);
                }

                mMaxStage = ((stageNum) / 5 + 1).ToString() + "_" + ((stageNum + 1) % 5).ToString();
                Debug.Log("MaxStage " + mMaxStage);

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
                case 1:
                    if(!SoundDirector.Instance.PlayBGM("TitleSound", true))
                    {
                        SoundDirector.Instance.StopCurBGM();
                        SoundDirector.Instance.PlayBGM("TitleSound", false);
                    }
                    break;
                case 2:
                    if (!SoundDirector.Instance.PlayBGM("TitleSound", true))
                    {
                        SoundDirector.Instance.StopCurBGM();
                        SoundDirector.Instance.PlayBGM("TitleSound", false);
                    }
                    break;
                case 3:
                    int chapter = mFileName[0] - 48;
                    switch(chapter)
                    {
                        case 1:
                            SoundDirector.Instance.StopCurBGM();
                            SoundDirector.Instance.PlayBGM("Chapter1 Sound", false);
                            break;
                        case 2:
                            SoundDirector.Instance.StopCurBGM();
                            SoundDirector.Instance.PlayBGM("Chapter2 Sound", false);
                            break;
                        case 3:
                            SoundDirector.Instance.StopCurBGM();
                            SoundDirector.Instance.PlayBGM("Chapter3 Sound", false);
                            break;
                    }

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
                case 4:
                    if (!SoundDirector.Instance.PlayBGM("TitleSound", true))
                    {
                        SoundDirector.Instance.StopCurBGM();
                        SoundDirector.Instance.PlayBGM("TitleSound", false);
                    }
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
                SoundDirector.Instance.update();

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

        Debug.Log("Write GameData File");
        writer.WriteLine(mScoreDic.Count - 1);
        foreach(var iter in mScoreDic)
        {
            if(iter.Value != -1)
            {
                writer.WriteLine(iter.Value);
                Debug.Log("WriteFile Stage " + iter.Key + " Score " + iter.Value);
            }
        }
        writer.Close();
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
        PlayerDirector.Instance.StopPlayer();
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

        if(!mScoreDic.ContainsKey(GetNextStage(mFileName)))
            mScoreDic.Add(GetNextStage(mFileName), -1);

        GamePause();
        mGameCleared = true;
        mDeathCount = 0;
    }

    public void GamePause()
    {
        PlayerDirector.Instance.StopPlayer();
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

    public bool IsOpend(int fChapter, int fStage)
    {
        string stage = fChapter.ToString() + "_" + fStage.ToString();

        return mScoreDic.ContainsKey(stage);
    }
    #endregion
}
