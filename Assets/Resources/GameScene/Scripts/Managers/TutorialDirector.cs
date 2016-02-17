using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialDirector : Singletone<TutorialDirector>
{
    [SerializeField]
    private UnityEngine.UI.Text mText = null;
    private float mOpacityActTime = 0.5f;
    private List<string> mTutorialTexts = new List<string>();

    void Start()
    {
        Color col = mText.color;
        col.a = 0;
        mText.color = col;
        var reader = FileIODirector.ReadFile("Datas\\TutorialText.gamedata");

        while(reader.Peek() != -1)
        {
            mTutorialTexts.Add(reader.ReadLine());
        }
    }

    public void ShowText(int fIndex)
    {
        if(fIndex >= 0 && fIndex < mTutorialTexts.Count)
        {
            StopAllCoroutines();
            mText.text = mTutorialTexts[fIndex];
            StartCoroutine(AppearText());
        }
    }

    public void HideText()
    {
        StopAllCoroutines();
        StartCoroutine(HideTextCor());
    }

    private IEnumerator AppearText()
    {
        float time = 0f;
        float oriA = mText.color.a;
        float targetTime = (1 - mText.color.a) * mOpacityActTime;

        while(time <= targetTime)
        {
            time += Time.deltaTime;
            Color col = mText.color;
            float rate = time / targetTime;
            col.a = Mathf.Lerp(oriA, 1, rate);
            mText.color = col;

            yield return null;
        }

        StopCoroutine(AppearText());
    }

    private IEnumerator HideTextCor()
    {
        float time = 0f;
        float oriA = mText.color.a;
        float targetTime = (mText.color.a) * mOpacityActTime;

        while (time <= targetTime)
        {
            time += Time.deltaTime;
            Color col = mText.color;
            float rate = time / targetTime;
            col.a = Mathf.Lerp(oriA, 0, rate);
            mText.color = col;

            yield return null;
        }

        StopCoroutine(HideTextCor());
    }
}
