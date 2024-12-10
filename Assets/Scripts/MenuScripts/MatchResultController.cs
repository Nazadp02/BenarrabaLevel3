using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchResultController : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject winCanvas;

    void Start()
    {
        // Asegurarte de que ambos Canvas están desactivados al iniciar la escena.
        gameOverCanvas.SetActive(false);
        winCanvas.SetActive(false);

        if (GameManager.Instance.PlayerWon) 
        {
            winCanvas.SetActive(true);
        }
        else
        {
            gameOverCanvas.SetActive(true);
        }
    }

    public void GoBackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
