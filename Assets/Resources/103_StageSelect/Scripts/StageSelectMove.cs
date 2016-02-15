using UnityEngine;
using System.Collections;

public class StageSelectMove : MonoBehaviour {

    public float Speed = 10.0f;
    public bool StageMoveRChek = false;
    public bool StageMoveLChek = false;

    Vector3 StageVec;
    float StageMoveNum = 0;
    float StagNum = 432;
    public Canvas can = null;
    float ScreenWidth = 0;

    void Start () {
        StageVec = GameObject.Find("StageSelect").transform.localPosition;
        StagNum = Screen.width;
        Speed = Screen.width;
	}

    public void RightMove()
    {
        if (StageMoveLChek == false)
        {
            if (StageMoveRChek != true)
            {
                StageMoveNum = StageVec.x;
            }
            StageMoveRChek = true;
        }
    }

    public void LeftMove()
    {
        if (StageMoveRChek == false)
        {
            if (StageMoveLChek != true)
            {
                StageMoveNum = StageVec.x;
            }
            StageMoveLChek = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (StageMoveRChek == true)
        {
            Debug.Log("StageChek");
            if (StageVec.x >= StageMoveNum - StagNum)
            {
                Debug.Log("If_OK");
                GameObject.Find("StageSelect").transform.localPosition = new Vector3(StageVec.x -= Speed * Time.deltaTime, StageVec.y, StageVec.z);
            }
            else
            {
                StageMoveRChek = false;
                GameObject.Find("StageSelect").transform.localPosition = new Vector3(StageVec.x = StageMoveNum - StagNum, StageVec.y, StageVec.z);
                StageMoveNum = StageVec.x;
            }

        }
        else if(StageMoveLChek == true)
        {
            if (StageVec.x <= StageMoveNum + StagNum)
            {
                GameObject.Find("StageSelect").transform.localPosition = new Vector3(StageVec.x += Speed * Time.deltaTime, StageVec.y, StageVec.z);
            }
            else
            {
                StageMoveLChek = false;
                GameObject.Find("StageSelect").transform.localPosition = new Vector3(StageVec.x = StageMoveNum + StagNum, StageVec.y, StageVec.z);
                StageMoveNum = StageVec.x;
            }
        }
	
	}
}
