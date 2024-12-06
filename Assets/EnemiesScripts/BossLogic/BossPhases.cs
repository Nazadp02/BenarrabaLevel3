using UnityEngine;

public class BossPhases : MonoBehaviour
{
    public enum BossState { PhaseOne, PhaseTwo }
    public BossState currentState = BossState.PhaseOne;

    [Header("Phase 1")]
    [SerializeField] private WeakPoint[] weakPoints; // Lista de puntos débiles

    [Header("Phase 2")]
    [SerializeField] private GameObject minionsPrefab; // Prefab de los enemigos a spawnear
    [SerializeField] private Transform[] spawnPoints; // Puntos de spawn para los enemigos
    [SerializeField] private int enemiesToSpawn = 5; // Número de enemigos a spawnear en la segunda fase

    private int destroyedWeakPoints = 0;

    void Start()
    {
        // Inicializar estado del jefe
        currentState = BossState.PhaseOne;
    }

    public void RegisterWeakPointDestroyed()
    {
        destroyedWeakPoints++;

        // Transición a la segunda fase si se destruyen 2 puntos débiles
        if (destroyedWeakPoints >= 2 && currentState == BossState.PhaseOne)
        {
            TransitionToPhaseTwo();
        }
    }

    private void TransitionToPhaseTwo()
    {
        currentState = BossState.PhaseTwo;

        // Hacer inmortal al jefe (por ejemplo, desactivando su componente de salud)
        BossHealth bossHealth = GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            bossHealth.IsInvulnerable = true;
        }

        // Spawnear enemigos
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(minionsPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
