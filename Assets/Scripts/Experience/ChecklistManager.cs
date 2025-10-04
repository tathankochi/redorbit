using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class ChecklistManager : MonoBehaviour
{
    [Header("Data Sources")]
    public MissionData missionData;       // ScriptableObject with mission list
    public string saveFile = "missions.json";

    [Header("UI Setup")]
    public GameObject togglePrefab;       // Prefab with Toggle + TMP_Text
    public Transform checklistParent;     // Panel with VerticalLayoutGroup

    private MissionProgress progress = new MissionProgress();
    private Dictionary<string, Toggle> missionToggles = new Dictionary<string, Toggle>();

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

            // ✅ Make toggle read-only
            toggle.interactable = false;

            // ✅ Check only if mission is completed in JSON
            toggle.isOn = progress.completed.Contains(mission);

            missionToggles[mission] = toggle;
        }
    }
    public void RefreshChecklist()
{
    LoadProgress(); // reload JSON
    foreach (var mission in missionData.missionDescriptions)
    {
        if (missionToggles.ContainsKey(mission))
        {
            missionToggles[mission].isOn = progress.completed.Contains(mission);
        }
    }
}
}

[System.Serializable]
public class MissionProgress
{
    public List<string> completed;
}
