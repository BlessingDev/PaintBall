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
        float rate = mTime / mStartEffectTime;
        CameraDirector.Instance.mFollowSpeed = CustomMath.Lerp(mCamStartSpeed, mOriCameraSpeed, rate);

        if(rate >= 1f)
        {
            mStartEffect = false;
            CameraDirector.Instance.mFollowSpeed = mOriCameraSpeed;
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
        CameraDirector.Instance.mFollowSpeed = CameraDirector.Instance.Distance * 0.003f;
        GameDirector.Instance.GamePause();

        Debug.Log("camera " + Camera.main.transform.position + " OriCam " + mOriCameraSpeed + " StartCam " + mCamStartSpeed + " CameraTime " + mStartEffectTime);

        mStartEffect = true;
    }
    #endregion
}
