using UnityEngine;
using System.Collections;

public class ShootingDirector : Singletone<ShootingDirector>
{
    public Transform mShootPos = null;

    [SerializeField]
    private float mShootSpeed = 10f;
    [SerializeField]
    private int mMaxBullet = 10;
    private GameObject mBullet = null;
    private Vector2 mShootDirection = Vector2.zero;
    private Bullet[] mBullets = null;
    private int mCurBulletIndex = 0;
    private int mCurBulletNum = 0;

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
        CheckMouse();
        UpdateBullets();
    }

    private void CheckMouse()
    {
        if(Input.GetMouseButton(0) && !UIDirector.Instance.mUITouched)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject arm = PlayerDirector.Instance.Arm;

            float deg = Mathf.Atan2(mousePos.y - arm.transform.position.y, mousePos.x - arm.transform.position.x);
            arm.transform.localEulerAngles = new Vector3(0, 0, deg * Mathf.Rad2Deg + 90);

            mShootDirection = (mousePos - (Vector2)arm.transform.position).normalized;
        }

        if(Input.GetMouseButtonUp(0))
        {
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
        if(mCurBulletNum < mMaxBullet)
        {
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
