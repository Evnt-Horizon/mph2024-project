using UnityEngine;
using UnityEngine.SceneManagement;  // Pour charger des sc�nes
using UnityEngine.UI;  // Pour interagir avec les UI elements

public class MainMenu : MonoBehaviour
{
    // Ces m�thodes sont appel�es par les boutons UI
    public void StartGame()
    {
        // Charger la sc�ne de jeu (assurez-vous que la sc�ne de jeu est ajout�e � la Build Settings)
        SceneManager.LoadScene("LucasScene");
    }

    public void OpenSettings()
    {
        // Ouvre une sc�ne de param�tres ou affiche un menu contextuel
        Debug.Log("Ouvrir les param�tres");
    }

    public void QuitGame()
    {
        // Quitte le jeu
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}
