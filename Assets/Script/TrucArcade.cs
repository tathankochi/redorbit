using UnityEngine;

public class TrucArcade : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    public static TrucArcade instance;
    public Dialog dialog;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        if (!startPlaying)
        {
            startPlaying = true;
            theBS.hasStarted = true;

            if (theMusic != null)
            {
                theMusic.Play();
            }
            else
            {
                Debug.LogError("theMusic is not assigned in the Inspector!");
            }
        }
    }

    void OnButtonClicked()
    {
        gameObject.SetActive(false);
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