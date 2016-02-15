using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapDirector : Singletone<MapDirector>
{
    #region Variables
    [SerializeField]
    private Vector2 mTileSize = Vector2.zero;
    private Vector2 mMapSize = Vector2.zero;
    [SerializeField]
    private Vector2 mOffset = Vector2.zero;
    private List<Tile> mTiles = new List<Tile>();
    private Dictionary<string, GameObject> mTilePrefabDic = new Dictionary<string, GameObject>();
    private Vector2 mCheckPoint = Vector2.zero;
    private float mMapDepth = 0f;
    private float mReveilTime = 1f;
    private GameObject mExit = null;
    private int mStar3 = 0;
    private int mStar2 = 0;
    private int mStar1 = 0;
    private bool mInitialized = false;
    #endregion

    #region Capsules
    public Vector2 MapSize
    {
        get
        {
            return mMapSize;
        }
    }
    public Vector2 TileSize
    {
        get
        {
            return mTileSize;
        }
    }
    public Vector2 CheckPoint
    {
        set
        {
            mCheckPoint = value;
        }
    }
    public float MapDepth
    {
        get
        {
            return mMapDepth;
        }
    }
    public float ReveilTime
    {
        get
        {
            return mReveilTime;
        }
    }
    public GameObject ExitTile
    {
        get
        {
            return mExit;
        }
    }
    #endregion

    #region VirtualFunctions
    // Use this for initialization
    void Start ()
    {
        if(!mInitialized)
        {
            mInitialized = true;
            var tilePres = Resources.LoadAll<GameObject>("GameScene/Prefabs/TilePrefabs/");
            Debug.Log("tilePres " + tilePres.Length);

            for (int i = 0; i < tilePres.Length; i++)
            {
                Debug.Log("tile name " + tilePres[i].name + " saved");
                mTilePrefabDic.Add(tilePres[i].name, tilePres[i]);
            }
        }
    }
	
    /// <summary>
    /// 게임 다이렉터가 매 프레임마다 부를 업데이트 함수
    /// </summary>
    public void update()
    {
        foreach(var iter in mTiles)
        {
            iter.update();
        }
    }
    #endregion

    #region CustomFunctions

    public void LoadMap(string mFileName)
    {
        if (!mInitialized)
            Start();

        var reader = FileIODirector.ReadFile("Maps\\" + mFileName + ".mapdata");

        if(reader != null)
        {
            mMapSize.x = System.Convert.ToInt32(reader.ReadLine());
            mMapSize.y = System.Convert.ToInt32(reader.ReadLine());
            GameDirector.Instance.BulletLimit = System.Convert.ToInt32(reader.ReadLine());
            mStar3 = System.Convert.ToInt32(reader.ReadLine());
            mStar2 = System.Convert.ToInt32(reader.ReadLine());
            mStar1 = System.Convert.ToInt32(reader.ReadLine());

            string data = reader.ReadToEnd();

            int index = 0;

            for(int i = (int)mMapSize.y - 1; i >= 0; i--)
            {
                for(int j = 0; j < mMapSize.x; j++)
                {
                    try
                    {
                        index = LoadTile(data, index, new Vector2(j, i));
                    }
                    catch(System.Exception ex)
                    {
                        index++;
                        Debug.LogWarning(ex.Message);
                    }
                }
                index += 2;
            }
        }
    }

    private Vector2 GetPositionFromMapIndex(Vector2 fMapIndex)
    {
        Vector2 pos = new Vector2((fMapIndex.x * mTileSize.x) + (mTileSize.x / 2), (fMapIndex.y * mTileSize.y) + (mTileSize.y / 2));
        pos += mOffset;

        return pos;
    }

    private int LoadTile(string fData, int fIndex, Vector2 fMapIndex)
    {
        char tileCode = fData[fIndex];
        Debug.Log("code " + tileCode + "@ index " + fMapIndex);
        GameObject obj = null;
        GameObject tile = null;
        string num = "";

        switch(tileCode)
        {
            case '0':

                return fIndex + 1;
            #region case3
            case '3':
                obj = null;
                tile = null;
                if (mTilePrefabDic.TryGetValue(tileCode.ToString(), out obj))
                {
                    tile = Instantiate(obj) as GameObject;
                    Debug.Log("tilePos " + GetPositionFromMapIndex(fMapIndex));
                    tile.transform.position = GetPositionFromMapIndex(fMapIndex);

                }
                else
                {
                    Debug.LogWarning("Could Not Find " + tileCode.ToString() + " from PrefabDictionary");
                }

                MoveTile moveTile = tile.GetComponent<MoveTile>();
                moveTile.mStartPos = tile.transform.position;

                fIndex += 1;
                num = "";
                for(; fData[fIndex] != '!'; fIndex++)
                {
                    num += fData[fIndex];
                }
                fIndex += 1;

                moveTile.mStopTime = (float)System.Convert.ToDouble(num);
                num = "";
                
                Vector2 end = moveTile.mStartPos;
                if(fData[fIndex] == 'x')
                {
                    fIndex += 1;
                    for (; fData[fIndex] != '!'; fIndex++)
                    {
                        num += fData[fIndex];
                    }
                    fIndex += 1;

                    float x = (float)System.Convert.ToDouble(num);

                    end.x = x;
                }
                else if(fData[fIndex] == 'y')
                {
                    fIndex += 1;
                    for (; fData[fIndex] != '!'; fIndex++)
                    {
                        num += fData[fIndex];
                    }
                    fIndex += 1;

                    float y = (float)System.Convert.ToDouble(num);
                    Debug.Log("y " + y);

                    end.y = y;
                }
                Debug.Log("end " + end);
                moveTile.mEndPos = end;

                for (; fData[fIndex] != '!'; fIndex++)
                {
                    num += fData[fIndex];
                }
                fIndex += 1;
                Debug.Log("num" + num);

                moveTile.mMoveTime = (float)System.Convert.ToDouble(num);

                mTiles.Add(moveTile);

                return fIndex;
            #endregion
            case '4':
                if (mTilePrefabDic.TryGetValue(tileCode.ToString(), out obj))
                {
                    tile = Instantiate(obj) as GameObject;
                    Debug.Log("tilePos " + GetPositionFromMapIndex(fMapIndex));
                    tile.transform.position = GetPositionFromMapIndex(fMapIndex);

                }
                else
                {
                    Debug.LogWarning("Could Not Find " + tileCode.ToString() + " from PrefabDictionary");
                }

                fIndex += 1;
                num = "";
                for (; fData[fIndex] != '!'; fIndex++)
                {
                    num += fData[fIndex];
                }
                fIndex += 1;

                TriggerReload trigger = tile.GetComponent<TriggerReload>();

                trigger.mReload = System.Convert.ToInt32(num);
                mTiles.Add(tile.GetComponent<Tile>());

                return fIndex;
            case '5':
                obj = null;
                if (mTilePrefabDic.TryGetValue(tileCode.ToString(), out obj))
                {
                    tile = Instantiate(obj) as GameObject;
                    Debug.Log("tilePos " + GetPositionFromMapIndex(fMapIndex));
                    tile.transform.position = GetPositionFromMapIndex(fMapIndex);

                }
                else
                {
                    Debug.LogWarning("Could Not Find " + tileCode.ToString() + " from PrefabDictionary");
                }

                mExit = tile;
                mTiles.Add(tile.GetComponent<Tile>());
                return fIndex + 1;
            case '1':
            case '2':
            case '6':
            case '7':
            case '8':
                obj = null;
                if (mTilePrefabDic.TryGetValue(tileCode.ToString(), out obj))
                {
                    tile = Instantiate(obj) as GameObject;
                    Debug.Log("tilePos " + GetPositionFromMapIndex(fMapIndex));
                    tile.transform.position = GetPositionFromMapIndex(fMapIndex);
                    
                }
                else
                {
                    Debug.LogWarning("Could Not Find " + tileCode.ToString() + " from PrefabDictionary");
                }

                mTiles.Add(tile.GetComponent<Tile>());
                return fIndex + 1;
            case '@':
                PlayerDirector.Instance.MakePlayer(GetPositionFromMapIndex(fMapIndex));
                mCheckPoint = GetPositionFromMapIndex(fMapIndex);

                return fIndex + 1;
            default:
                throw new System.ArgumentException("The Tile Code Isn't Defined", "fTileNum");
        }
    }

    public void UnveilTiles(List<GameObject> fList)
    {
        Debug.Log("unveil Tiles");
        foreach(var iter in fList)
        {
            iter.GetComponent<Tile>().OnUnveil();
        }
    }
    
    public void MoveToSavePoint()
    {
        try
        {
            PlayerDirector.Instance.Player.transform.position = mCheckPoint;
        }
        catch(System.Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }

    public int GetScore()
    {
        int death = GameDirector.Instance.DeathCount;

        if (death <= mStar3)
            return 3;
        else if (death <= mStar2)
            return 2;
        else if (death <= mStar1)
            return 1;
        else
            return 0;
    }
    #endregion
}
