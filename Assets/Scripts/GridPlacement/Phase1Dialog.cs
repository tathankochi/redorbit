using UnityEngine;

public class Phase1Dialog : MonoBehaviour
{
    [SerializeField]
    private Dialog dialog;
    void Start()
    {
        dialog.gameObject.SetActive(true);
        dialog.ResetDialog();
        dialog.dialogLines = new string[] {
            "Press SPACE to continue ...",
            "You will start with basic modules and connectors.",
            "Connect modules to create a functional space station.",
            "You have to connect surrounding modules to the core module.",
            "Remember, each module has specific connection points.",
            "Plan your layout carefully to ensure the design is at least reasonable.",
        };
        dialog.StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
