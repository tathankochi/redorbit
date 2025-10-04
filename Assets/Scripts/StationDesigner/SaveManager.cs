using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class SaveManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class SaveData
    {
        [System.Serializable]
        public class PlacedData
        {
            public string prefabName;
            public Vector3 position;
        }
        public List<PlacedData> items = new List<PlacedData>();
    }
    [SerializeField]
    private GameObject[] modules = new GameObject[10];
    [SerializeField]
    private Tilemap[] tilemaps = new Tilemap[10];
    private GameObject[] furniture;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Save()
    {
        var data = new SaveData();
        foreach (var item in modules)
        {
            if (item == null) continue;
            Vector3 position = item.transform.position;
            data.items.Add(new SaveData.PlacedData()
            {
                prefabName = item.name,
                position = position
            });
            Debug.Log("Saved item: " + item.name + " at position: " + position);
        }
        foreach (var tilemap in tilemaps)
        {
            if (tilemap == null) continue;
            Vector3 position = tilemap.transform.position;
            data.items.Add(new SaveData.PlacedData()
            {
                prefabName = tilemap.name,
                position = position
            });
            Debug.Log("Saved tilemap: " + tilemap.name + " at position: " + position);
        }
        furniture = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (var item in furniture)
        {
            if (item == null) continue;
            Vector3 position = item.transform.position;
            data.items.Add(new SaveData.PlacedData()
            {
                prefabName = item.name,
                position = position
            });
            Debug.Log("Saved furniture: " + item.name + " at position: " + position);
        }
        var json = JsonUtility.ToJson(data, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Game Saved to " + Application.persistentDataPath + "/savefile.json");
    }
}
