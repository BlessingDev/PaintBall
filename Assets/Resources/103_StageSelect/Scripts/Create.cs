using UnityEngine;
using System.Collections;

public class Create : MonoBehaviour {//크래딧이나 옵션뛰우는 창

    public void Pop()
    {
        transform.localPosition = new Vector3(0, 0, 1);
    }
    public void Destory()
    {
        transform.localPosition = new Vector3(4000, 0, 1);
    }

}
