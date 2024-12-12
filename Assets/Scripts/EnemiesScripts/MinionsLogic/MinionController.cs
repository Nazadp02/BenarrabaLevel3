using SmallHedge.SoundManager;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    #region Components

    [Header("Enemy Data")]
    [SerializeField] private int maxLife = 10;
    [SerializeField] private int damage = 5;

    private int currentLife;
    private bool isChasing;
    private NavMeshAgent agent;
    private Transform playerTransform;
    private Animator animator;


    #endregion

    #region UnityFuntions

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(30, 70);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();    
        currentLife = maxLife;
    }

    private void Update()
    {
        SearchPlayer();
    }

    #endregion

    #region GoToPlayer

    /// <summary>
    /// enemy go search and go toward the player
    /// </summary>
    private void SearchPlayer()
    {
        NavMeshHit hit;

        // Check if there are no obstacles between enemy and player
        if (!agent.Raycast(playerTransform.position, out hit))
        {
            agent.stoppingDistance = 1.5f;

            float distance = Vector3.Distance(transform.position, playerTransform.position);

            if (distance > agent.stoppingDistance)
            {
                // Chase the player
                animator.SetBool("move", true);
                animator.SetBool("attack", false);
                agent.isStopped = false;
                agent.SetDestination(playerTransform.position);
            }
            else
            {
                // Stop and attack
                agent.isStopped = true;
                animator.SetBool("move", false);
                FacePlayer(); // Ensure enemy faces the player
                MeleeAttack();
            }
        }
    }

    /// <summary>
    /// Enemy faces the player
    /// </summary>
    private void FacePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0; // Keep rotation on the horizontal plane
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    #endregion

    #region Attack

    private void MeleeAttack()
    {
        animator.SetBool("attack", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Está haciendo daño al jugador");

            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    #endregion

    #region Health

    /// <summary>
    /// Handle when the enemy receive an attack of the player
    /// </summary>
    /// <param name="quantity"></param>
    public void DamageEnemy(int quantity)
    {
        currentLife -= quantity;
        if (currentLife <= 0/* || GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().IsDead == true*/)
        {
            SoundManager.PlaySound(SoundType.ENEMYHIT, volume: 0.2f);
            Destroy(gameObject);
        }
    }

    #endregion
}
