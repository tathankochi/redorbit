using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Khởi tạo scene bằng cách load dữ liệu từ file save
/// Khôi phục vị trí module, furniture, tilemap từ savefile.json
/// Gán tilemap cho module tương ứng
/// </summary>
public class Starter : MonoBehaviour
{
    /// <summary>
    /// Mảng chứa các module prefab
    /// </summary>
    [SerializeField] private GameObject[] modules = new GameObject[10];
    /// <summary>
    /// Mảng chứa các furniture prefab
    /// </summary>
    [SerializeField] private GameObject[] furniture;
    /// <summary>
    /// Mảng chứa các tilemap prefab
    /// </summary>
    [SerializeField] private Tilemap[] tilemaps = new Tilemap[10];

    /// <summary>
    /// Khởi tạo scene: Load dữ liệu từ savefile.json
    /// Khôi phục vị trí tất cả object đã lưu
    /// </summary>
    void Start()
    {
        // Load positions from save file
        var path = Application.persistentDataPath + "/savefile.json";
        if (!System.IO.File.Exists(path))
        {
            Debug.Log("No save file found at " + path);
            return;
        }

        var json = System.IO.File.ReadAllText(path);
        var data = JsonUtility.FromJson<SaveManager.SaveData>(json);

        foreach (var item in data.items)
        {
            // Find the prefab by name
            GameObject prefab = FindPrefabByName(item.prefabName);
            if (prefab == null)
            {
                Debug.LogWarning("Prefab not found for " + item.prefabName);
                continue;
            }

            // Instantiate the tilemap at the saved position as a child of the Grid gameObject
            if (prefab.GetComponent<Tilemap>() != null)
            {
                Debug.Log("Instantiating tilemap " + item.prefabName);
                // check if there is a game object named "Grid" in the scene
                if (GameObject.Find("Grid") == null)
                {
                    Debug.LogError("No GameObject named 'Grid' found in the scene. Please add one to parent the tilemaps.");
                    continue;
                }
                Instantiate(prefab, item.position, Quaternion.identity, GameObject.Find("Grid").transform);
                Module[] sceneModules = FindObjectsByType<Module>(FindObjectsSortMode.None);
                Debug.Log("Current tilemap: " + prefab.name);
                foreach (var module in sceneModules)
                {
                    Debug.Log("Checking module " + module.name + " for tilemap assignment");
                    Debug.Log("Looking for tilemap named: " + "Tilemap_" + module.name);
                    if (prefab.name + "(Clone)" == "Tilemap_" + module.name)
                    {
                        module.moduleTilemap = prefab.GetComponent<Tilemap>();
                        Debug.Log("Assigned tilemap to module " + module.name);
                        break;
                    }
                }
                continue;
            }
            else
            {
                // Instantiate furniture/module bình thường
                Instantiate(prefab, item.position, Quaternion.identity);
            }
            // Assign the tilemap to the module's tilemap if it has the same name
            // find list of modules in the scene

            // Debug.Log("Instantiated " + item.prefabName + " at " + item.position);
        }
    }

    /// <summary>
    /// Tìm prefab theo tên trong các mảng
    /// Tìm trong modules array trước, sau đó furniture, cuối cùng tilemaps
    /// </summary>
    /// <param name="prefabName">Tên prefab cần tìm</param>
    /// <returns>GameObject prefab hoặc null nếu không tìm thấy</returns>
    private GameObject FindPrefabByName(string prefabName)
    {
        // Search in the modules array
        foreach (var module in modules)
        {
            if (module != null && module.name == prefabName)
            {
                return module;
            }
        }

        // Search in the furniture array
        foreach (var item in furniture)
        {
            if (item != null && item.name == prefabName)
            {
                return item;
            }
        }
        // Search in the tilemaps array
        foreach (var tilemap in tilemaps)
        {
            if (tilemap != null && tilemap.name == prefabName)
            {
                return tilemap.gameObject;
            }
        }

        return null; // Return null if no matching prefab is found
    }
}