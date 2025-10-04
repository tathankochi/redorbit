using UnityEngine;

public class SecondDialog : MonoBehaviour
{
    [SerializeField]
    private Dialog dialog;
    private int count;
    private string jsonFile;
    void Start()
    {
        jsonFile = Application.persistentDataPath + "/count.json";
        count = JsonFileIO.ReadCountJson(jsonFile);
        if (count == 1)
        {
            dialog.gameObject.SetActive(true);
            dialog.ResetDialog();
            dialog.dialogLines = new string[] {
            "Excellent work! You've completed the first loop and successfully restored the water flow!",
            "All three of our seedlings are very happy and have grown significantly!",
            "Growing crops in space is a long-term journey. Now, we move on to the next phase of",
            "For these young plants to develop into mature, fruit-bearing crops,",
            "you'll need to overcome further challenges involving light, nutrient delivery, and the microgravity environment.",
            "Keep up the good work, and you will see your garden become even greener and more productive!",
            "Good luck, Astronaut!"
        };
        dialog.StartDialog();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

