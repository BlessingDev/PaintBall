using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D mRB = null;

    public void Start()
    {
        Debug.Log("bullet start");
        mRB = GetComponent<Rigidbody2D>();
    }

    public void AddForce(Vector2 force)
    {
        mRB.AddForce(force);
    }
}
