using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Audio/Audio Settings")]
public class AudioSettings : ScriptableObject
{
    [Header("Music Settings")]
    public AudioClip backgroundMusic;
    public float musicVolume = 0.7f;
    public bool musicEnabled = true;
    
    [Header("SFX Settings")]
    public float sfxVolume = 1.0f;
    public bool sfxEnabled = true;
    
    // Phương thức để lưu cài đặt
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetInt("MusicEnabled", musicEnabled ? 1 : 0);
        PlayerPrefs.SetInt("SFXEnabled", sfxEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    // Phương thức để load cài đặt
    public void LoadSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        sfxEnabled = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;
    }
}
