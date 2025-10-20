using UnityEngine;

public class PlayerMovement : MonoBehaviour {
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

    // GONZA
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip saltoClip;
    [SerializeField] private AudioClip[] pasosClips;        //  clips de pasos
    [SerializeField] private float pasoIntervalo = 0.5f;    //  tiempo entre pasos

    private float pasoTimer;        // controla el tiempo entre pasos
    // GONZA

    private CharacterController controller;
    private Vector3 velocidadCaida;
    private bool estaEnSuelo;

    void Start() {
        controller = GetComponent<CharacterController>();
        if (controller == null) {
            Debug.LogError("¡El CharacterController no está asignado en el GameObject!");
        }
        // Si no asignaste el Animator en el inspector, intenta buscarlo
        if (animator == null) {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
                Debug.LogWarning("No se encontró Animator. Las animaciones no funcionarán.");
        }
        // GONZA
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>(); // por si lo olvidás asignar
        // GONZA
    }

    void Update() {
        // Verificar si está en el suelo
        estaEnSuelo = Physics.CheckSphere(puntoSuelo.position, radioSuelo, capaSuelo);

        // Aplicar gravedad
        if (estaEnSuelo && velocidadCaida.y < 0) {
            velocidadCaida.y = -2f; // Evita acumulación excesiva
        }

        // Salto
        if (Input.GetButtonDown("Jump") && estaEnSuelo) {
            Debug.Log("salto!");
            velocidadCaida.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);
            animator?.SetTrigger("jump");

            // GONZA
            if (audioSource != null && saltoClip != null)
                audioSource.PlayOneShot(saltoClip);
            // GONZA
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

        // GONZA
        if (estaEnSuelo && magitudMovimiento > 0.1f)
        {
            pasoTimer -= Time.deltaTime;
            if (pasoTimer <= 0f)
            {
                ReproducirPaso();
                // Si corre, pasos mas rapidos
                pasoTimer = corriendo ? pasoIntervalo * 0.6f : pasoIntervalo;
            }
        }
        else
        {
            pasoTimer = 0f; // reiniciar si deja de moverse o salta
        }
        // GONZA

        // Animaciones
        if (animator != null) {
            // Normalizar la entrada para evitar que caminar diagonal sea más rápido
            bool estaMoviendo = magitudMovimiento > 0.1f;

            if (estaEnSuelo) {
                // Solo uno sera TRUE a la ves
                animator.SetBool("IsWalking", estaMoviendo && !corriendo);
                animator.SetBool("IsRunning", estaMoviendo && corriendo);

                if (estaMoviendo) {
                    if (corriendo) {
                        animator.SetBool("IsWalking", false);
                        animator.SetBool("IsRunning", true);
                    } else {
                        animator.SetBool("IsWalking", true);
                        animator.SetBool("IsRunning", false);
                    }
                } else {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", false);
                }
            }
        }

        // Aplicar gravedad    
        velocidadCaida.y += gravedad * Time.deltaTime;
        controller.Move(velocidadCaida * Time.deltaTime);

    }

    private void ReproducirPaso()
    {
        if (pasosClips == null || pasosClips.Length == 0 || audioSource == null) return;

        AudioClip clip = pasosClips[Random.Range(0, pasosClips.Length)];
        audioSource.PlayOneShot(clip);
    }
}