using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class ChecklistManager : MonoBehaviour
{
    [Header("Data Sources")]
    public MissionData missionData;
    public string saveFile = "missions.json";

    [Header("UI Setup")]
    public GameObject togglePrefab;
    public Transform checklistParent;

    private MissionProgress progress = new MissionProgress();
    private Dictionary<string, Toggle> missionToggles = new Dictionary<string, Toggle>();
    public GameObject finishGameButton;

    void Awake()
    {
        LoadProgress();
        BuildChecklist();
    }

    public void LoadProgress()
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

    public void BuildChecklist()
    {
        foreach (string mission in missionData.missionDescriptions)
        {
            GameObject obj = Instantiate(togglePrefab, checklistParent);
            Toggle toggle = obj.GetComponent<Toggle>();
            TMP_Text label = toggle.GetComponentInChildren<TMP_Text>();

            label.text = mission;

            toggle.interactable = false;

            toggle.isOn = progress.completed.Contains(mission);

            missionToggles[mission] = toggle;
        }
    }

    public void RefreshChecklist()
    {
        LoadProgress();
        foreach (var mission in missionData.missionDescriptions)
        {
            if (missionToggles.ContainsKey(mission))
            {
                missionToggles[mission].isOn = progress.completed.Contains(mission);
            }
        }
    }
    public bool IsAllMissionsCompleted()
    {
        foreach (string mission in missionData.missionDescriptions)
        {
            if (!progress.completed.Contains(mission))
            {
                return false;
            }
        }
        return true;
    }
    public void Update()
    {
        if (IsAllMissionsCompleted())
        {
            Debug.Log("All missions completed!");
            finishGameButton.SetActive(true);
        }
    }
}

[System.Serializable]
public class MissionProgress
{
    public List<string> completed;
}
