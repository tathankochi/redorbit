using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    private AudioSource audioSource;
    
    void Awake()
    {
        // Đảm bảo chỉ có một instance duy nhất
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Không xóa khi chuyển scene
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Xóa instance cũ nếu đã tồn tại
        }
    }
    
    // Phương thức để bật/tắt nhạc
    public void ToggleMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
        else
            audioSource.UnPause();
    }
    
    // Phương thức để tắt nhạc nền
    public void StopBackgroundMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
    
    // Phương thức để bật lại nhạc nền
    public void ResumeBackgroundMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
    
    // Phương thức để kiểm tra nhạc có đang phát không
    public bool IsMusicPlaying()
    {
        return audioSource != null && audioSource.isPlaying;
    }
    
    // Phương thức để thay đổi âm lượng
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
    
    // Phương thức để thay đổi nhạc
    public void ChangeMusic(AudioClip newClip)
    {
        audioSource.clip = newClip;
        audioSource.Play();
    }
}
