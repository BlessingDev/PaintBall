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
    private float mOriCameraSpeed = 0f;
    #endregion

    #region VirtualFunctions
    void Start()
    {
        mBulletEffects = new PaintEffect[mMaxBulletEffect];
    }

    public void update()
    {
        if (mStartEffect)
            StartEffectUpdate();
    }
    #endregion

    #region CustomFunctions
    private void StartEffectUpdate()
    {
        if (CameraDirector.Instance.Distance <= 0.01f)
        {
            mStartEffect = false;
            CameraDirector.Instance.mFollowSpeed = mOriCameraSpeed;
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
        Camera.main.transform.position = MapDirector.Instance.ExitTile.transform.position;
        mOriCameraSpeed = CameraDirector.Instance.mFollowSpeed;
        CameraDirector.Instance.mFollowSpeed = 0.003f * CameraDirector.Instance.Distance;

        mStartEffect = true;
    }
    #endregion
}
