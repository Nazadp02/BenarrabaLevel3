using UnityEngine;
using UnityEngine.AI;

public class BossAttack : MonoBehaviour
{

    #region Components

    //shoot properties 
    [Header("Attack Settings")]
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private Transform proyectileShootPoint;
    [SerializeField] private GameObject proyectile;

    [Header("Movement Settings")]
    [SerializeField] private int shootToMove = 4;   // Número máximo de disparos antes de moverse
    [SerializeField] private GameObject[] possiblePositions; // Puntos posibles donde el enemigo puede moverse

    //private components
    private float nextShootTime = 0f;
    private int shootCount = 0;
    private bool isShootingProyectile;

    private NavMeshAgent agent;
    private Animator animator;

    private Transform playerTransform;

    #endregion

    #region UnityFuntions

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    private void Update()
    {
        ChangeBasicAnimations();

        // Si es el momento de disparar, llama a la animacion de ataque que tiene un evento que llama a lanzar proyectil
        if (Time.time >= nextShootTime && agent.velocity.sqrMagnitude <= 0.1f)
        {
            animator.SetBool("attack", true);
            isShootingProyectile = true;

            nextShootTime = Time.time + attackRate;
        }
    }

    #endregion

    #region Shoot

    private void ShootProjectile()
    {
        if (isShootingProyectile)
        {

            // Instancia el proyectil y ajusta la dirección hacia el jugador
            GameObject projectile = Instantiate(proyectile, proyectileShootPoint.position, proyectileShootPoint.rotation);

            //llamamos al script para que gestione el movimiento del proyectil
            ProyectileController guidedProjectile = projectile.GetComponent<ProyectileController>();

            //incrementamos el número de disparos
            shootCount++;

            // Si el enemigo ha disparado 4 proyectiles, cambia de posición
            if (shootCount >= shootToMove)
            {
                MoveToNewPosition();
                shootCount = 0; // Resetear contador
            }
        }
    }


    #endregion

    #region Movement

    private void MoveToNewPosition()
    {
        isShootingProyectile = false;

        // Elige un nuevo punto al azar de los posibles
        if (possiblePositions.Length > 0)
        {
            int randomIndex = Random.Range(0, possiblePositions.Length);
            Vector3 destination = possiblePositions[randomIndex].transform.position;

            agent.SetDestination(destination);
        }
    }


    private void GenerateRandomShotsToMove()
    {
        // Generar un número aleatorio entre 4 y 8
        shootToMove = Random.Range(4, 9); // El rango superior en Random.Range es exclusivo
    }

    #endregion

    #region Animations

    private void ChangeBasicAnimations()
    {
        // Verifica si el enemigo está moviéndose
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            animator.SetBool("IsMoving", true); // Cambiar la animación a "caminar"
            animator.SetBool("attack", false);

        }
        else
        {
            LookAtPlayer();
            animator.SetBool("IsMoving", false); // Cambiar la animación a "detenido"
        }

    }

    private void LookAtPlayer()
    {
        // Rotar al enemigo para que mire al jugador
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);  // Rotación suave
    }

    #endregion

}

