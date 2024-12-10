using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject optionsMenu; 
    [SerializeField] private GameObject CreditsMenu; 
    [SerializeField] private GameObject mainMenu;  

    private bool isOptionsMenuOpen = false;
    private bool isCreditsOpen = false;

    private void Start()
    {
        optionsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PushPlay()
    {
        LoadSceneManager.instance.LoadScene("GameScene");
    }

    public void PushQuitGame()
    {
        Application.Quit();
    }

    public void ToggleOptionsMenu() //alternar menu de opciones seg�n este activado o no
    {
        isOptionsMenuOpen = !isOptionsMenuOpen; //si est� activado lo desactiva y si est� desactivado lo activa

        // Mostrar/ocultar men�s
        optionsMenu.SetActive(isOptionsMenuOpen);
        mainMenu.SetActive(!isOptionsMenuOpen);
    }

    public void ToggleCredits() 
    {
        isCreditsOpen = !isCreditsOpen; 

        // Mostrar/ocultar men�s
        CreditsMenu.SetActive(isCreditsOpen);
        mainMenu.SetActive(!isCreditsOpen);
    }
}
