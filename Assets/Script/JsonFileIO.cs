using UnityEngine;
using System.IO;

public class JsonFileIO : MonoBehaviour
{
    [System.Serializable]
    private class CountData
    {
        public int count;
    }

    public static int ReadCountJson(string filename)
    {
        try
        {
            if (File.Exists(filename))
            {
                string json = File.ReadAllText(filename);
                CountData data = JsonUtility.FromJson<CountData>(json);
                Debug.Log($"Read {filename}");
                return (data.count);
            }
        }
        catch
        {
            // Trả về 0 nếu file không tồn tại hoặc lỗi
        }
        return (0);
    }

    public static void WriteCountJson(string filename, int count)
    {
        CountData data = new CountData {count = count};
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filename, json);
        Debug.Log($"Write {filename}");
    }
}
