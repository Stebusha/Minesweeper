using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverState : MonoBehaviour
{
    public static GameOverState Instance1 { get; private set; }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
    }
    private void Awake()
    {
        Instance1 = this;
        Hide();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
