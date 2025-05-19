using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorRobot : MonoBehaviour
{
    private Animator animator;          // Referencia al componente Animator para controlar las animaciones del personaje
    public GameObject proyectilPrefab; // Prefab del proyectil (una esfera) que se va a lanzar
    public Transform puntoDisparo;     // Punto desde donde se lanza el proyectil (un objeto vac�o posicionado en la mano u otra parte)

    public float fuerzaLanzamiento = 10f; // Fuerza horizontal (hacia adelante) con la que se lanza el proyectil
    float fuerzaVertical = 5f; // Fuerza vertical (hacia arriba) del lanzamiento
    public float velocidadMovimiento = 5f;

    public AudioClip sonidoDisparo; // Asigna en Inspector
    private AudioSource audioSource;

    public Button botonReiniciar; // Asigna este en el inspector
    private bool puedeDisparar = true; // Controla si puede disparar

    // M�todo que se ejecuta una vez al iniciar el juego
    void Start()
    {
        // Obtiene la referencia al componente Animator
        animator = GetComponent<Animator>();

        // Reproduce la animaci�n inicial (idle en modo de pelea)
        animator.Play("fight_idle");
        audioSource = GetComponent<AudioSource>();
        if (botonReiniciar != null)
            botonReiniciar.gameObject.SetActive(false); // Ocultar al inicio
    }

    // M�todo que se ejecuta en cada frame
    void Update()
    {
        VerificarAros(); // Cada frame revisa si quedan aros

        // Movimiento lateral (izquierda y derecha)
        float movimientoHorizontal = Input.GetAxis("Horizontal"); // A/D o Flechas
        Vector3 desplazamiento = new Vector3(movimientoHorizontal, 0, 0);
        transform.Translate(desplazamiento * velocidadMovimiento * Time.deltaTime);



        // Si el jugador presiona la tecla Espacio
        if (puedeDisparar && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Se presion� espacio");

            // Dispara la animaci�n de ataque usando un trigger en el Animator
            animator.SetTrigger("PlayAttack");

            // Sonido de Disparo
            audioSource.PlayOneShot(sonidoDisparo);

            // Crea una nueva instancia del proyectil en la posici�n y rotaci�n del punto de disparo
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

            // Calcula la direcci�n hacia adelante con base en el personaje (no la mano)
            Vector3 direccion = transform.forward; // 'transform' es el personaje

            // Obtiene el Rigidbody del proyectil para aplicar fuerzas f�sicas
            Rigidbody rb = proyectil.GetComponent<Rigidbody>();

            // Resetea la velocidad y rotaci�n para evitar acumulaci�n de fuerzas
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (rb != null)
            {

                // Calcula el impulso: hacia adelante m�s una elevaci�n vertical
                Vector3 fuerzaImpulso = transform.forward * fuerzaLanzamiento + Vector3.up * fuerzaVertical;
                // Aplica la fuerza como un impulso (empuje instant�neo)
                rb.AddForce(fuerzaImpulso, ForceMode.Impulse);

            }
        }
    }
    void VerificarAros()
    {
        GameObject[] aros = GameObject.FindGameObjectsWithTag("Aro");
        if (aros.Length == 0)
        {
            puedeDisparar = false;

            if (botonReiniciar != null)
                botonReiniciar.gameObject.SetActive(true); // Mostrar bot�n reinicio
        }
    }
}
