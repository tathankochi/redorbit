using UnityEngine;

public class Dialog1 : MonoBehaviour
{
    [SerializeField]
    private Dialog dialog;
    private int count;
    private string jsonFile;
    void Start()
    {
        jsonFile = Application.persistentDataPath + "/count.json";
        count = JsonFileIO.ReadCountJson(jsonFile);
        if (count == 0)
        {
            dialog.gameObject.SetActive(true);
            dialog.ResetDialog();
            dialog.dialogLines = new string[] {
            "Welcome, Astronaut...",
            "I am EVA, your plant support system.",
            "Your mission is to nurture this tiny garden, our most valuable source of food and oxygen in space.",
            "Take a look! These are our first three sprouts...",
            "We have Vegetable Lettuce, successfully harvested by astronauts on the ISS;",
            "Española Pepper, the first fruit crop grown up here;",
            "and finally, Russian Red Kale, a highly nutritious superfood!",
            "To help them grow, we need a stable water supply. Your first challenge is that the hydration pipeline system is currently",
            "Get ready! Press the “P” key on your keyboard to start and fix the pipeline!"
        };
        dialog.StartDialog();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
