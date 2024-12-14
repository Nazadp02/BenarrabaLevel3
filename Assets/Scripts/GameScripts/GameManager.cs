using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool PlayerWon { get; private set; }

    private void Awake()
    {
        // Singleton para acceso global.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndGame(bool won)
    {
        PlayerWon = won;
        SceneManager.LoadScene("WinGameoverScene");
    }
}
