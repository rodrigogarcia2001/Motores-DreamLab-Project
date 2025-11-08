using UnityEngine;

public class DestroyOnInteract : MonoBehaviour, IInteractable
{
    public string mensaje = "Presiona E para Agarrar";



    public void Interactuar()
    {
        Debug.Log("Interact with this");
        // Destruye el objeto inmediatamente
        Destroy(this.gameObject);
    }

    public void OnSeleccionado()
    {
        // Notificar al jugador para que muestre el mensaje
        PlayerInteraction jugador = FindObjectOfType<PlayerInteraction>();
        if (jugador != null)
        {
            jugador.MostrarTexto(mensaje);
        }
    }

    public void OnDeseleccionado()
    {
        PlayerInteraction jugador = FindObjectOfType<PlayerInteraction>();
        if (jugador != null)
        {
            jugador.OcultarTexto();
        }
    }
}