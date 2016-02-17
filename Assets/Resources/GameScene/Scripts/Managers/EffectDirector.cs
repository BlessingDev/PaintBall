using UnityEngine;
using System.Collections;

public class EffectDirector : Singletone<EffectDirector>
{
    #region Variables
    [SerializeField]
    private int mMaxBulletEffect = 5;
    private PaintEffect[] mBulletEffects = null;
    private int mCurBulletIndex = 0;
    /// <summary>
    /// 시작 이펙트가 실행 중인가
    /// </summary>
    private bool mStartEffect = false;
    private float mStartEffectTime = 1f;
    private float mOriCameraSpeed = 0f;
    private float mCamStartSpeed = 0f;
    private float mTime = 0f;
    /// <summary>
    /// y = ax^2의 a
    /// </summary>
    private float mA = 1f;
    #endregion

    #region VirtualFunctions
    void Start()
    {
        mBulletEffects = new PaintEffect[mMaxBulletEffect];
    }

    void Update()
    {
        if (mStartEffect)
            StartEffectUpdate();
    }

    public void update()
    {

    }
    #endregion

    #region CustomFunctions
    private void StartEffectUpdate()
    {
        Debug.Log("StarEffectUpdate");
        CameraDirector.Instance.update();
        
        mTime += Time.deltaTime;
        if(mTime <= mStartEffectTime)
        {
            CameraDirector.Instance.mFollowSpeed = mA * mTime * mTime;
            Debug.Log("Time " + mTime + " Speed " + CameraDirector.Instance.mFollowSpeed);
        }
        
        if(Mathf.Abs(CameraDirector.Instance.mFollowSpeed - mOriCameraSpeed) <= 2.8f)
        {
            mStartEffect = false;
            GameDirector.Instance.GameResume();
        }
    }

    private PaintEffect GetCurPaintEffect()
    {
        for (int i = 0; i < mMaxBulletEffect; i++)
        {
            if (!mBulletEffects[mCurBulletIndex].gameObject.activeSelf)
            {
                int idx = mCurBulletIndex;
                mCurBulletIndex = (mCurBulletIndex + 1) % mMaxBulletEffect;
                return mBulletEffects[idx];
            }

            mCurBulletIndex = (mCurBulletIndex + 1) % mMaxBulletEffect;
        }
        return null;
    }

    public void BulletEffect(Bullet fBullet, Transform fOther)
    {
        var effect = GetCurPaintEffect();
        Vector2 tileSize = MapDirector.Instance.TileSize;

        float xDif = Mathf.Abs(fBullet.transform.position.x - fOther.position.x);
        float yDif = Mathf.Abs(fBullet.transform.position.y - fOther.position.y);

        //x방향으로 맞았다.
        if(Mathf.Abs(tileSize.x / 2 - xDif) < Mathf.Abs(tileSize.y / 2 - yDif))
        {

        }
    }

    public void StartEffect()
    {
        mTime = 0f;
        Camera.main.transform.position = MapDirector.Instance.ExitTile.transform.position;
        mOriCameraSpeed = CameraDirector.Instance.mFollowSpeed;
        CameraDirector.Instance.mFollowSpeed = 0;
        mStartEffectTime = CameraDirector.Instance.Distance * 0.5f;
        GameDirector.Instance.GamePause();
        mA = mOriCameraSpeed / (mStartEffectTime * mStartEffectTime);

        Debug.Log("camera " + Camera.main.transform.position + " OriCam " + mOriCameraSpeed + " StartCam " + mCamStartSpeed + " CameraTime " + mStartEffectTime);

        mStartEffect = true;
    }
    #endregion
}
