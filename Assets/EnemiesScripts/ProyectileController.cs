using UnityEngine;

public class ProyectileController : MonoBehaviour
{

    #region Components

    [Header("Proyectile Info")]
    [SerializeField] private float proyectileSpeed = 10f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float destroyTime = 5f;

    private Rigidbody rb;
    private Transform playerTransform;

    public int Damage { get => damage; set => damage = value; }

    #endregion

    #region UnityFuntions

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        ProyectileMovement();
    }

    #endregion

    private void ProyectileMovement()
    {
        // Calcula la dirección hacia el jugador
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Aplica una rotación suave para que el proyectil se oriente hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

        // Mueve el proyectil hacia el jugador
        rb.velocity = transform.forward * proyectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);

            //TODO Player_TakeDamage
        }
    }
}
