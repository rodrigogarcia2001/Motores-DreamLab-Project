using UnityEngine;
using TMPro;

public class FloatingTextItem : MonoBehaviour
{
    [Header("Tiempo que se muestra el texto")]
    public float displayTime = 5f;

    private TMP_Text floatingText;
    private bool shown = false;

    private void Awake()
    {
        // Busca automáticamente un TextMeshPro 3D hijo
        floatingText = GetComponentInChildren<TMP_Text>();
        if (floatingText != null)
            floatingText.gameObject.SetActive(false); // empieza oculto
        else
            Debug.LogWarning("No se encontró ningún TMP_Text como hijo de " + gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (shown) return;

        if (other.CompareTag("Player"))
        {
            shown = true;
            ShowText();
        }
    }

    void ShowText()
    {
        if (floatingText != null)
        {
            floatingText.gameObject.SetActive(true);
            Invoke(nameof(HideText), displayTime);
        }
    }

    void HideText()
    {
        if (floatingText != null)
            floatingText.gameObject.SetActive(false);

        shown = false;
    }

    private void Update()
    {
        // Hace que el texto flote sobre el cubo y mire a la cámara
        if (floatingText != null && floatingText.gameObject.activeSelf)
        {
            floatingText.transform.position = transform.position + Vector3.up * 2f;
            floatingText.transform.rotation = Quaternion.LookRotation(floatingText.transform.position - Camera.main.transform.position);
        }
    }
}