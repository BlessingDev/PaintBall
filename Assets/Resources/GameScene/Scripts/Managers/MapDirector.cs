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
    #endregion

    #region Capsules
    public Vector2 MapSize
    {
        get
        {
            return mMapSize;
        }
    }
    #endregion

    #region VirtualFunctions
    // Use this for initialization
    void Start ()
    {
        var tilePres =  Resources.LoadAll<GameObject>("GameScene/Prefabs/TilePrefabs/");
        Debug.Log("tilePres " + tilePres.Length);

        for(int i = 0; i < tilePres.Length; i++)
        {
            mTilePrefabDic.Add(tilePres[i].name, tilePres[i]);
        }

        LoadMap("SampleMap");
    }
	
    /// <summary>
    /// 게임 다이렉터가 매 프레임마다 부를 업데이트 함수
    /// </summary>
    public void update()
    {

    }
    #endregion

    #region CustomFunctions

    public void LoadMap(string mFileName)
    {
        var reader = FileIODirector.ReadFile("Maps\\" + mFileName + ".mapdata");

        if(reader != null)
        {
            mMapSize.x = System.Convert.ToInt32(reader.ReadLine());
            mMapSize.y = System.Convert.ToInt32(reader.ReadLine());

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

        switch(tileCode)
        {
            case '0':

                return fIndex + 1;
            case '1':
            case '2':
                GameObject obj = null;
                if (mTilePrefabDic.TryGetValue(tileCode.ToString(), out obj))
                {
                    GameObject tile = Instantiate(obj) as GameObject;
                    Debug.Log("tilePos " + GetPositionFromMapIndex(fMapIndex));
                    tile.transform.position = GetPositionFromMapIndex(fMapIndex);
                    
                }
                else
                {
                    Debug.LogWarning("Could Not Find " + tileCode.ToString() + " from PrefabDictionary");
                }

                return fIndex + 1;
            case '@':
                PlayerDirector.Instance.MakePlayer(GetPositionFromMapIndex(fMapIndex));

                return fIndex + 1;
            default:
                throw new System.ArgumentException("The Tile Code Isn't Defined", "fTileNum");
        }
    }

    public void SetTileDepth(List<GameObject> fList)
    {
        foreach(var iter in fList)
        {
            iter.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }


    #endregion
}
