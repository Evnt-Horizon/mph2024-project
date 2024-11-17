using UnityEngine;
using UnityEngine.SceneManagement;  // Pour charger des scènes
using UnityEngine.UI;  // Pour interagir avec les UI elements

public class MainMenu : MonoBehaviour
{
    // Ces méthodes sont appelées par les boutons UI
    public void StartGame()
    {
        // Charger la scène de jeu (assurez-vous que la scène de jeu est ajoutée à la Build Settings)
        SceneManager.LoadScene("LucasScene");
    }

    public void OpenSettings()
    {
        // Ouvre une scène de paramètres ou affiche un menu contextuel
        Debug.Log("Ouvrir les paramètres");
    }

    public void QuitGame()
    {
        // Quitte le jeu
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}
