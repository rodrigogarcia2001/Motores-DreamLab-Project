using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 6f;           //Velocidad al caminar
    public float velocidadCarrera = 10f;   //Velocidad al Correr
    public float gravedad = -35f;
    public float fuerzaSalto = 3f;

    [Header("Suelo")]
    public Transform puntoSuelo;
    public float radioSuelo = 0.4f;
    public LayerMask capaSuelo;

    [Header("Animaciones")]
    public Animator animator;

    private CharacterController controller;
    private Vector3 velocidadCaida;
    private bool estaEnSuelo;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("¡El CharacterController no está asignado en el GameObject!");
        }
        // Si no asignaste el Animator en el inspector, intenta buscarlo
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
                Debug.LogWarning("No se encontró Animator. Las animaciones no funcionarán.");
        }
    }

    void Update()
    {
        // Verificar si está en el suelo
        estaEnSuelo = Physics.CheckSphere(puntoSuelo.position, radioSuelo, capaSuelo);

        // Aplicar gravedad
        if (estaEnSuelo && velocidadCaida.y < 0)
        {
            velocidadCaida.y = -2f; // Evita acumulación excesiva
        }

        // Salto
        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            velocidadCaida.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);
            animator?.SetTrigger("jump");
        }

        // Movimiento horizontal
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direccion = transform.right * horizontal + transform.forward * vertical;
        float magitudMovimiento = direccion.magnitude;

        // Determinar si está corriendo
        bool corriendo = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float velocidadActual = corriendo ? velocidadCarrera : velocidad;

        // Aplicar movimiento
        controller.Move(direccion * velocidadActual * Time.deltaTime);

        // Animaciones
        if (animator != null)
        {
            // Normalizar la entrada para evitar que caminar diagonal sea más rápido
            bool estaMoviendo = magitudMovimiento > 0.1f;

            if (estaEnSuelo)
            {
                // Solo uno sera TRUE a la ves
                animator.SetBool("IsWalking", estaMoviendo && !corriendo);
                animator.SetBool("IsRunning", estaMoviendo && corriendo);

                if (estaMoviendo)
                {
                    if (corriendo)
                    {
                        animator.SetBool("IsWalking", false);
                        animator.SetBool("IsRunning", true);
                    }
                    else
                    {
                        animator.SetBool("IsWalking", true);
                        animator.SetBool("IsRunning", false);
                    }
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", false);
                }
            }
        }

        // Aplicar gravedad    
        velocidadCaida.y += gravedad * Time.deltaTime;
        controller.Move(velocidadCaida * Time.deltaTime);
    }
}