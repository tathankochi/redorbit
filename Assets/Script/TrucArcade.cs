using UnityEngine;

public class TrucArcade : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    public static TrucArcade instance;
    public Dialog dialog;
    private bool backgroundMusicWasPlaying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Lưu trạng thái nhạc nền trước khi tắt
        if (MusicManager.Instance != null)
        {
            backgroundMusicWasPlaying = MusicManager.Instance.IsMusicPlaying();
            // Tắt nhạc nền khi vào scene TrucArcade
            MusicManager.Instance.StopBackgroundMusic();
        }
    }
    
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
    
    // Phương thức để bật lại nhạc nền khi rời khỏi TrucArcade
    public void ResumeBackgroundMusic()
    {
        if (MusicManager.Instance != null && backgroundMusicWasPlaying)
        {
            MusicManager.Instance.ResumeBackgroundMusic();
        }
    }
    
    // Gọi khi GameObject bị destroy hoặc disable
    void OnDestroy()
    {
        ResumeBackgroundMusic();
    }
    
    void OnDisable()
    {
        ResumeBackgroundMusic();
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