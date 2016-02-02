using UnityEngine;
using System.Collections;

public class ShootingDirector : Singletone<ShootingDirector>
{
    #region Variables
    public Transform mShootPos = null;
    public float mDegOffset = 0f;

    [SerializeField]
    private float mShootSpeed = 10f;
    [SerializeField]
    private int mMaxBullet = 10;
    [SerializeField]
    private GameObject mBullet = null;
    [SerializeField]
    private UnityEngine.UI.Image mLine = null;
    [SerializeField]
    private UnityEngine.UI.Image mPoint = null;
    private Vector2 mShootDirection = Vector2.zero;
    private Bullet[] mBullets = null;
    private int mCurBulletIndex = 0;
    private int mCurBulletNum = 0;
    private bool mAimed = false;
    private int mDegOffsetMul = 1;
    #endregion

    void Start()
    {
        mBullet = Resources.Load("GameScene\\Prefabs\\Bullet") as GameObject;
        mBullets = new Bullet[mMaxBullet];

        for(int i = 0; i < mMaxBullet; i++)
        {
            mBullets[i] = (Instantiate(mBullet) as GameObject).GetComponent<Bullet>();
            mBullets[i].gameObject.SetActive(false);
            mBullets[i].Start();
        }
    }

    public void update()
    {
        UpdateBullets();
    }

    public void lateUpdate()
    {
        CheckMouse();
    }

    private void CheckMouse()
    {
        if(Input.GetMouseButton(0) && !UIDirector.Instance.mUITouched)
        {
            mAimed = true;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject arm = PlayerDirector.Instance.Arm;

            float deg = Mathf.Atan2(mousePos.y - arm.transform.position.y, mousePos.x - arm.transform.position.x) * Mathf.Rad2Deg;
            arm.transform.localEulerAngles = new Vector3(0, arm.transform.localEulerAngles.y, (deg + mDegOffset) * mDegOffsetMul);
            mLine.gameObject.SetActive(true);
            mLine.transform.position = Camera.main.WorldToScreenPoint(mShootPos.position);
            mLine.transform.localEulerAngles = new Vector3(mLine.transform.localEulerAngles.x, mLine.transform.localEulerAngles.y, deg);

            mShootDirection = (mousePos - (Vector2)mShootPos.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(mShootPos.position, mShootDirection, 100f, (1 << LayerMask.NameToLayer("Tile")));

            if(hit.collider != null)
            {
                Debug.Log("hit");
                mPoint.gameObject.SetActive(true);
                mPoint.transform.position = Camera.main.WorldToScreenPoint(hit.point);
            }
            else
            {
                mPoint.gameObject.SetActive(false);
            }

            if(deg< 0)
            {
                deg += 360;
            }

            if(deg >= 90 && deg <= 270)
            {
                mDegOffset = 180;
                mDegOffsetMul = -1;
                PlayerDirector.Instance.Player.transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                mDegOffset = 0;
                mDegOffsetMul = 1;
                PlayerDirector.Instance.Player.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

        }

        if(Input.GetMouseButtonUp(0) && mAimed)
        {
            mAimed = false;
            mLine.gameObject.SetActive(false);
            ShootBullet();
        }
    }

    private void UpdateBullets()
    {
        for(int i = 0; i < mMaxBullet; i++)
        {
            if(mBullets[i].gameObject.activeSelf)
            {
                ScreenOutTest(mBullets[i].gameObject);
            }
        }
    }

    private void ScreenOutTest(GameObject fObj)
    {
        Vector2 scenePos = Camera.main.WorldToScreenPoint(fObj.transform.position);
        Vector2 screenSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

        if(scenePos.x < 0 || scenePos.x > screenSize.x ||
            scenePos.y < 0 || scenePos.y > screenSize.y)
        {
            mCurBulletNum--;
            fObj.SetActive(false);
        }
    }

    private void ShootBullet()
    {
        if(mCurBulletNum < mMaxBullet && GameDirector.Instance.mBulletLimit > 0)
        {
            GameDirector.Instance.mBulletLimit--;
            Bullet bullet = null;
            int idx = mCurBulletIndex;
            for (int i = 0; i < mMaxBullet; i++)
            {
                if(!mBullets[idx].gameObject.activeSelf)
                {
                    bullet = mBullets[idx];
                    mCurBulletIndex = idx;
                    break;
                }
                idx = (idx + 1) % mMaxBullet;
            }

            bullet.gameObject.SetActive(true);
            bullet.transform.position = mShootPos.position;
            bullet.AddForce(mShootDirection * mShootSpeed);

            mCurBulletIndex = (mCurBulletIndex + 1) % mMaxBullet;
            mCurBulletNum++;
        }
        else
        {
            Debug.LogWarning("All Bullets are Used");
        }
    }

    public void BulletDisable(Bullet fBullet)
    {
        for(int i = 0; i < mMaxBullet; i++)
        {
            if(mBullets[i].Equals(fBullet))
            {
                mBullets[i].gameObject.SetActive(false);
                mCurBulletNum--;
                break;
            }
        }
    }
}
