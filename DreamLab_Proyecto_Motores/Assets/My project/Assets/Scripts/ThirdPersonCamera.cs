using System;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform objetivo; // Arrastra aquí al jugador

    [Header("Configuración de la cámara")]
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Posición relativa detrás y arriba del jugador
    public float suavidad = 5f; // Cuán suave es el seguimiento (mayor = más suave)

    [Header("Colisión de cámara")]
    public LayerMask capaObstaculos; // Paredes, Techos, etc
    public float distanciaMinima = 0.5f; // Distancia minima al jugador

    [Header("Rotación con el ratón")]
    public bool rotarConRaton = true;
    public float sensibilidadX = 2f;

    void Start()
    {
        if (objetivo == null)
        {
            Debug.LogError("¡Asigna un objetivo (jugador) en el inspector!");
        }

        // Bloquear cursor para FPS/TPS
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        
        rotateCamera();
        // Obtener la rotación actual del jugador (solo Y)
        Quaternion rotacionObjetivo = Quaternion.Euler(0f, objetivo.eulerAngles.y, 0f);

        // Calcular posición deseada de la cámara (Sin obstáculo)
        Vector3 posicionDeseada = objetivo.position + rotacionObjetivo * offset;

        // Dirección desde la cámara ideal hacia el jugador
        Vector3 direccionACamara = posicionDeseada - objetivo.position;
        float distanciaDeseada = direccionACamara.magnitude;

        // Raycast desde el jugador hacia la posición deseada de la cámara
        RaycastHit hit;
        if (Physics.Raycast(objetivo.position, direccionACamara.normalized, out hit, distanciaDeseada, capaObstaculos))
        {
            // Hay un obstáculo -> ajustar la cámara justo antes del obstáculo
            float distanciaObstaculo = hit.distance;
            if (distanciaObstaculo < distanciaMinima)
                distanciaObstaculo = distanciaMinima;

            // Nueva posición: desde el jugador, en dirección a la cámara, pero detenido antes del obstáculo
            Vector3 posicionAjustada = objetivo.position + direccionACamara.normalized * distanciaObstaculo;
            transform.position = Vector3.Lerp(transform.position, posicionAjustada, suavidad * Time.deltaTime);
        }
        else
        {
            // No hay obstáculos -> ir a la posición deseada
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavidad * Time.deltaTime);
        }

        // Siempre mirar al jugador
        transform.LookAt(objetivo);
    }

    private void rotateCamera() {
        // Rotación horizontal con el ratón (alrededor del jugador)
        if (rotarConRaton) {
            float mouseX = Input.GetAxis("Mouse X") * sensibilidadX;
            objetivo.Rotate(Vector3.up * mouseX);
        }
    }
}
