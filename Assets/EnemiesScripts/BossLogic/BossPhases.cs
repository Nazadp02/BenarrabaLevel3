using System.Collections.Generic;
using UnityEngine;

public class BossPhases : MonoBehaviour
{
    #region Components

    public enum BossState { PhaseOne, PhaseTwo, PhaseThree }
    public BossState currentState = BossState.PhaseOne;

    [Header("Phase 1")]
    [SerializeField] private WeakPoint[] weakPoints; // Lista de puntos débiles
    [SerializeField] private int totalWeakPoints = 4;

    [Header("Phase 2")]
    [SerializeField] private GameObject minionsPrefab; // Prefab de los enemigos a spawnear
    [SerializeField] private GameObject spawnPointsContainer;
    [SerializeField] private int enemiesToSpawn = 5; // Número de enemigos a spawnear en la segunda fase
    [SerializeField] private float minionSpawnRate = 10f; // Tiempo entre spawns de minions

    private int destroyedWeakPoints = 0;
    private bool areWeakPointDestroyed;
    private List<Transform> spawnPoints = new List<Transform>(); // Puntos de spawn para los enemigos

    private List<GameObject> spawnedEnemies = new List<GameObject>(); // Lista de enemigos spawneados
    private bool isCheckingForEnemies = false; // Controlar si estamos revisando enemigos
    private float nextMinionSpawnTime = 0f; // Controla el tiempo para spawnear minions

    private bool isInPhaseThree = false;

    public bool AreWeakPointDestroyed { get => areWeakPointDestroyed; set => areWeakPointDestroyed = value; }

    #endregion

    #region UnityFuntions

    void Start()
    {
        // Inicializar estado del jefe
        currentState = BossState.PhaseOne;

        foreach (Transform child in spawnPointsContainer.transform) spawnPoints.Add(child);
    }

    private void Update()
    {
        if (isCheckingForEnemies)
        {
            CheckEnemiesStatus();
        }

        if (isInPhaseThree)
        {
            // Spawnear minions periódicamente
            if (Time.time >= nextMinionSpawnTime)
            {
                SpawnEnemies();
                nextMinionSpawnTime = Time.time + minionSpawnRate;
            }
        }
    }

    #endregion

    #region ConditionsToTrantitions

    public void RegisterWeakPointDestroyed()
    {
        destroyedWeakPoints++;

        // Transición a la segunda fase si se destruyen 2 puntos débiles
        if (destroyedWeakPoints == 2 && currentState == BossState.PhaseOne)
        {
            TransitionToPhaseTwo();
        }

        // Transición a la tercera fase
        if (destroyedWeakPoints >= totalWeakPoints)
        {
            TransitionToPhaseThree();
        }
    }

    private void CheckEnemiesStatus()
    {
        // Eliminar enemigos destruidos de la lista
        spawnedEnemies.RemoveAll(enemy => enemy == null);

        // Si la lista está vacía, todos los enemigos han sido destruidos
        if (spawnedEnemies.Count == 0)
        {
            isCheckingForEnemies = false;
            TransitionToPhaseOne();
        }
    }

    #endregion

    #region PhaseOne

    private void TransitionToPhaseOne()
    {
        currentState = BossState.PhaseOne;

        BossHealth bossHealth = GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            bossHealth.IsInvulnerable = false;
        }

        //vuelve a atacar
        BossAttack bossAttack = GetComponent<BossAttack>();
        if (bossAttack != null)
        {
            bossAttack.CanShoot = true;
            bossAttack.IsMoving = false;
            bossAttack.IsShootingProyectile = false;
        }        
    }

    #endregion

    #region PhaseTwo

    private void TransitionToPhaseTwo()
    {
        currentState = BossState.PhaseTwo;

        //deja de atacar
        BossAttack bossAttack = GetComponent<BossAttack>();
        if (bossAttack != null)
        {
            bossAttack.IsShootingProyectile = false;
            bossAttack.CanShoot = false;

        }

        // Spawnear enemigos
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject newEnemy = Instantiate(minionsPrefab, spawnPoint.position, spawnPoint.rotation);

            // Agregar enemigo a la lista
            spawnedEnemies.Add(newEnemy);
        }

        // Comienza a verificar si los enemigos han sido destruidos
        isCheckingForEnemies = true;
    }

    #endregion

    #region PhaseThree
    private void TransitionToPhaseThree()
    {
        isInPhaseThree = true;
        areWeakPointDestroyed = true;

        BossAttack bossAttack = GetComponent<BossAttack>();
        if (bossAttack != null)
        {
            bossAttack.IsShootingProyectile = true;
            bossAttack.CanShoot = true;
        }

        nextMinionSpawnTime = Time.time + minionSpawnRate;
    }

    #endregion
}
