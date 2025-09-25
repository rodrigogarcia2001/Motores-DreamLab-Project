using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuración")]
    public float distanciaInteraccion = 2f; // Distancia máxima para interactuar
    public LayerMask layerInteractuable;    // Solo detecta objetos en esta capa

    [Header("UI")]
    public TextMeshProUGUI textoInteraccion; // Usa TextMeshProUGUI

    private IInteractable objetoCercano;

    void Start()
    {
        OcultarTexto();
    }


    void Update()
    {
        // Raycast hacia adelante para detectar objetos interactuables
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaInteraccion, layerInteractuable))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // Si es un nuevo objeto, notificar que está enfocado
                if (objetoCercano != interactable)
                {
                    if (objetoCercano != null)
                        objetoCercano.OnDeseleccionado();

                    objetoCercano = interactable;
                    objetoCercano.OnSeleccionado();
                }

                // Si presiona la tecla de interacción
                if (Input.GetKeyDown(KeyCode.E))
                {
                    objetoCercano.Interactuar();
                }
            }
            else
            {
                // No hay objeto válido
                DeseleccionarObjeto();
            }
        }
        else
        {
            DeseleccionarObjeto();
        }
    }

    void DeseleccionarObjeto()
    {
        if (objetoCercano != null)
        {
            objetoCercano.OnDeseleccionado();
            objetoCercano = null;
        }
    }

    public void MostrarTexto(string mensaje = "Presiona E para interactuar")
    {
        if (textoInteraccion != null)
        {
            textoInteraccion.text = mensaje;
            textoInteraccion.gameObject.SetActive(true);
        }
    }

    public void OcultarTexto()
    {
        if (textoInteraccion != null)
        {
            textoInteraccion.gameObject.SetActive(false);
        }
    }

    // Opcional: dibujar el rayo en el editor para depuración
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * distanciaInteraccion);
    }
}