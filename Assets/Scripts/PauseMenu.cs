using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    [SerializeField] private GameObject PauseMenuUi;
    private void Awake()
    {
        GameIsPaused = false; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else if(Game.isLevelActive)
                Pause();
        }
    }

    public void LoadMenu()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        Game.isLevelActive = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
    public void Resume()
    {
        PauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Game.isLevelActive = true;
        bool stateMusic = true; ;
        if (PlayerPrefs.HasKey("IsVolume"))
        {
            if (PlayerPrefs.GetInt("IsVolume") == 1)
                stateMusic = true;
            else
                stateMusic = false;
        }
        AudioListener.pause = !stateMusic;
    }

    public void Pause()
    {
        PauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Game.isLevelActive = false;
        AudioListener.pause = true;

    }
}
