using UnityEngine;
using System.Collections;

public class Singletone<T> : MonoBehaviour where T : Singletone<T>
{
    private static T mInstance = null;

    static public T Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = FindObjectOfType<T>();
            }
            if(mInstance == null)
            {
                Debug.LogWarning("Singletone " + typeof(T).ToString() + " Does Not Exist on Hierarchy");
            }

            return mInstance;
        }
    }
}
