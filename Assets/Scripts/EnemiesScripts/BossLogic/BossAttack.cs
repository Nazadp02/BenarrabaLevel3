using System.Collections.Generic;
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
    [SerializeField] private GameObject possiblePositionsContainer;
    private List<Transform> possiblePositions = new List<Transform>(); // Puntos posibles donde el enemigo puede moverse

    //private components
    private float nextShootTime = 0f;
    private int shootCount = 0;
    private bool isShootingProyectile;
    private bool canShoot = true;
    private int shootToMove;

    private bool isMoving;
    private Transform currentWaypoint; // Punto actual al que se dirige
    private Transform previousWaypoint; // Punto previo para evitar repetición

    private NavMeshAgent agent;
    private Animator animator;

    private Transform playerTransform;

    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public bool IsShootingProyectile { get => isShootingProyectile; set => isShootingProyectile = value; }
    public bool CanShoot { get => canShoot; set => canShoot = value; }

    #endregion

    #region UnityFuntions

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        foreach (Transform child in possiblePositionsContainer.transform) possiblePositions.Add(child);

        // Asignar un valor aleatorio entre 4 y 9
        shootToMove = Random.Range(4, 10);
    }


    private void Update()
    {
        // Si es el momento de disparar, llama a la animacion de ataque que tiene un evento que llama a lanzar proyectil
        if (Time.time >= nextShootTime && agent.velocity.sqrMagnitude <= 0.1f && canShoot) //si está quiero y puede disparar (primera fase)
        {
            animator.SetBool("attack", true);
            isShootingProyectile = true;

            nextShootTime = Time.time + attackRate;
        }
        
        ChangeAnimations();

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            OnMovementComplete();  // Llamar a la función cuando el boss llegue al destino
            Debug.Log("Ha llegado al destino ");
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

            // Si el enemigo ha disparado los proyectiles correspondientes, cambia de posición
            if (shootCount >= shootToMove)
            {
                MoveToNewPosition();
                shootCount = 0; // Resetear contador        
                shootToMove = Random.Range(4, 7); // Asignar un nuevo valor aleatorio de disparos para la siguiente ronda
            }
        }
    }


    #endregion

    #region Movement

    private void MoveToNewPosition()
    {
        isShootingProyectile = false;
        isMoving = true;

        GetComponent<BossHealth>().IsInvulnerable = true;   //cuando se mueva no podrá recibir daño

        GetComponent<BossHealth>().StartBlink();

        // Excluye el waypoint actual de la lista
        List<Transform> possibleWaypoints = new List<Transform>(possiblePositions);
        possibleWaypoints.Remove(currentWaypoint);

        // Selecciona uno nuevo aleatoriamente
        Transform nextWaypoint = possibleWaypoints[Random.Range(0, possibleWaypoints.Count)];

        // Actualiza el waypoint anterior y el actual
        previousWaypoint = currentWaypoint;
        currentWaypoint = nextWaypoint;

        // Establece el nuevo destino
        agent.SetDestination(currentWaypoint.position);
        agent.isStopped = false;
    }

    private void LookAtPlayer()
    {
        GetComponent<BossHealth>().IsInvulnerable = true;
        isMoving = true;

        // Rotar al enemigo para que mire al jugador
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);  // Rotación suave

    }

    public void OnMovementComplete()
    {
        GetComponent<BossHealth>().StopBlink();

        isMoving = false;
    }

    #endregion

    #region Animations

    private void ChangeAnimations()
    {
        if (agent.velocity.sqrMagnitude <= 0.1f && canShoot == false) //si está quieto y no puede disparar (segunda fase)
        {
            agent.isStopped = true;
            animator.SetBool("idle", true);
            animator.SetBool("attack", false);
            animator.SetBool("IsMoving", false);
            GetComponent<BossHealth>().IsInvulnerable = true;
        }
        else if (agent.velocity.sqrMagnitude > 0.1f) //si está moviendose
        {
            animator.SetBool("IsMoving", true); // Cambiar la animación a "caminar"
            animator.SetBool("attack", false);
            animator.SetBool("idle", false);
        }
        else if (agent.velocity.sqrMagnitude <= 0.1f) //si está quieto
        {
            LookAtPlayer();
            animator.SetBool("IsMoving", false);
            GetComponent<BossHealth>().IsInvulnerable = false; // El boss deja de ser invulnerable
        }
    }

    #endregion

}

