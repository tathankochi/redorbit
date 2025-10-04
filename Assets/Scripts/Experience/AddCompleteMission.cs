using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class AddCompletedMission : MonoBehaviour
{
    [Header("Save File")]
    public string saveFile = "missions.json";

    private MissionProgress progress = new MissionProgress();

    void Awake()
    {
        LoadProgress();
    }

    public void MarkMissionComplete(string mission)
{
    if (!progress.completed.Contains(mission))
    {
        progress.completed.Add(mission);
        SaveProgress();
        Debug.Log($"Mission marked complete: {mission}");
    }
}

    private void LoadProgress()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFile);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            progress = JsonUtility.FromJson<MissionProgress>(json);
        }
        else
        {
            progress.completed = new List<string>();
        }
    }

    private void SaveProgress()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFile);
        string json = JsonUtility.ToJson(progress, true);
        File.WriteAllText(path, json);
    }
}
