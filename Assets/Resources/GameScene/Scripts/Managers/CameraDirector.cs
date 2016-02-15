using UnityEngine;
using System.Collections;

public class CameraDirector : Singletone<CameraDirector>
{
    private Camera mCamera = null;

    public bool mFollowPlayer = true;
    public float mFollowSpeed = 30f;
    public Vector2 mOffset = Vector2.zero;

    public float Distance
    {
        get
        {
            if (mCamera == null)
                Start();

            return Vector2.Distance(PlayerDirector.Instance.Player.transform.position, mCamera.transform.position);
        }
    }

	// Use this for initialization
	void Start ()
    {
        mCamera = Camera.main;
	}

    /// <summary>
    /// GameDirector에서 부를 update
    /// </summary>
    public void update()
    {
        SmoothCamera();
    }

    private void SmoothCamera()
    {
        if(mFollowPlayer)
        {
            Vector2 pos = Vector2.Lerp(mCamera.transform.position, 
                (Vector2)PlayerDirector.Instance.Player.transform.position 
                + mOffset, Time.deltaTime * 62.5f * mFollowSpeed);

            mCamera.transform.position = new Vector3(pos.x, pos.y, -10);
        }
    }
}
