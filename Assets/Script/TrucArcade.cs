using UnityEngine;

public class TrucArcade : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;

    public void StartGame()
    {
        if (!startPlaying)
        {

            startPlaying = true;
            theBS.hasStarted = true;

            theMusic.Play();
        }
    }
    public Dialog dialog;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("You are now exercising...");
        Dialog dialog = FindDialogInScene();
        if (dialog == null)
        {
            Debug.LogError("Dialog component not found in the scene.");
            return;
        }
        if (dialog.gameObject.activeSelf) return;
        dialog.ResetDialog();
        dialog.gameObject.SetActive(true);
        dialog.dialogLines = new string[] {
            "The game is controlled by Up/Down/Right/Left arrows",
            "Time it perfectly for the arrow to match the button and press it",
            "Have fun!",
            "Press SPACE to continue ..."
        };
        dialog.StartDialog();
    }
    private Dialog FindDialogInScene()
    {
        Dialog[] dialogs = Resources.FindObjectsOfTypeAll<Dialog>();
        foreach (Dialog dialog in dialogs)
        {
            if (dialog.gameObject.scene.IsValid()) // Ensure it's part of the active scene
            {
                return dialog;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void NoteHit()
    {
        Debug.Log("Hit On Time!");
    }
    public void NoteMissed()
    {
        Debug.Log("Missed Note!");
    }
}