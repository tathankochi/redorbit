using UnityEngine;
using System.IO;

public class DeleteOldMissions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Path to the missions.json file
        string jsonFile = Path.Combine(Application.persistentDataPath, "missions.json");

        // Check if the file exists
        if (File.Exists(jsonFile))
        {
            // Create an empty JSON structure with an empty "completed" array
            string emptyJson = "{ \"completed\": [] }";

            // Overwrite the file with the empty JSON structure
            File.WriteAllText(jsonFile, emptyJson);

            Debug.Log("Cleared old missions.json file");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}