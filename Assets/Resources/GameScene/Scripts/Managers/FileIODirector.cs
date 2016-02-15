using UnityEngine;
using System.Collections;
using System.IO;

public class FileIODirector
{
    /// <summary>
    /// Resources 폴더를 기준으로 해당 파일을 읽어온다
    /// </summary>
    /// <param name="fFileName">읽어올 파일의 주소</param>
    /// <returns>찾으면 파일의 StreamReader를 찾지 못하면 null을 반환한다</returns>
    public static StreamReader ReadFile(string fFileName)
    {
        StreamReader reader = null;
        string path = System.Environment.CurrentDirectory + "\\Assets\\Resources\\" + fFileName;

        reader = new StreamReader(path);

        if (reader == null)
            Debug.LogWarning("Couldn't Open File At " + path);

        return reader;
    }

    public static StreamWriter WriteFile(string fFileName)
    {
        StreamWriter writer = null;
        string path = System.Environment.CurrentDirectory + "\\Assets\\Resources\\" + fFileName;

        writer = new StreamWriter(path);

        if(writer == null)
            Debug.LogWarning("Couldn't Open Write At " + path);

        return writer;
    }
}
