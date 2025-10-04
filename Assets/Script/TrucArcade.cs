using UnityEngine;

public class TrucArcade : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    public static TrucArcade instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        instance = this;
    }

    void OnButtonClicked()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
        void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }
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