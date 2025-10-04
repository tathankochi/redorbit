using System.Collections.Generic;
using UnityEngine;

public class Phase2Dialog : MonoBehaviour
{
    [SerializeField]
    private Dialog dialog;
    [SerializeField]
    // private List<GameObject> furnitureButtons;
    private GameObject[] furnitureUIs = new GameObject[2];
    
    void Start()
    {
        
    }
    public void Show()
    {
        dialog.gameObject.SetActive(true);
        dialog.ResetDialog();
        dialog.dialogLines = new string[] {
            "Press SPACE to continue ...",
            "Great job! You have successfully connected modules using connectors.",
            "Now, let's add some equipment to your space station.",
        };
        dialog.StartDialog();
        foreach (GameObject element in furnitureUIs)
        {
            element.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
