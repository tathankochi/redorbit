using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        // Nếu đang rời khỏi scene TrucArcade, bật lại nhạc nền
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TrucArcade")
        {
            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.ResumeBackgroundMusic();
            }
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
