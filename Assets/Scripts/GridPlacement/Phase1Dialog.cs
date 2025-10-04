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
            "Humans are closer than ever to colonizing Mars.",
            "The journey to Mars was not like the trip to the Moon.",
            "Communication delay is over twenty minutes each way.",
            "This means that astronauts on Mars will have to be more autonomous.",
            "You are given a job to design a space station orbiting Mars.",
            "This is a step towards building a permanent base on Mars.",
            "You will start with basic modules and connectors."
        };
        dialog.StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
