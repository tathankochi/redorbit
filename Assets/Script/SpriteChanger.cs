using UnityEngine;
using System.IO;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] sprites; // Mảng các sprite trong Inspector
    public int count; // Biến count để chọn sprite
    private SpriteRenderer spriteRenderer;
    private string jsonFile;
    void Start()
    {
        jsonFile = Path.Combine(Application.persistentDataPath, "count.json");
        //JsonFileIO.WriteCountJson(jsonFile, 0);
        
        count = JsonFileIO.ReadCountJson(jsonFile); // Đọc count
        Debug.Log(count);
        // Lấy component SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Đảm bảo count nằm trong khoảng hợp lệ
        UpdateSprite();
    }

    void Update()
    {
         if (Input.GetKeyDown(KeyCode.P))
        {
            
             UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    void UpdateSprite()
    {
        // Kiểm tra count hợp lệ và thay đổi sprite
        if (sprites != null && sprites.Length > 0 && count >= 0 && count < sprites.Length)
        {
            spriteRenderer.sprite = sprites[count];
        }
    }
}
