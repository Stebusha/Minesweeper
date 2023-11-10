using UnityEngine;
using UnityEngine.SceneManagement;
public class GameWinState : MonoBehaviour
{
    public static GameWinState Instance2 { get; private set; }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    private void Awake()
    {
        Instance2 = this;
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
