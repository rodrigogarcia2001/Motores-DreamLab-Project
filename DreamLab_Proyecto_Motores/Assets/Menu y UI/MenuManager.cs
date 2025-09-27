using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Cargar escena por índice
    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Índice de escena inválido: " + sceneIndex);
        }
    }

    // Métodos específicos (opcionales, para arrastrar fácil en botones)
    public void Jugar() => LoadScene(1);      // Ir al nivel 1
    public void Menu() => LoadScene(0);  // Volver al menú
    public void VolverAJugar() => LoadScene(1);  // Reiniciar nivel
    public void Salir() => Application.Quit(); // Salir (solo funciona en build)
}