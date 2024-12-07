using System.Collections.Generic;
using UnityEngine;

public class BossPhases : MonoBehaviour
{
    #region Components

    public enum BossState { PhaseOne, PhaseTwo }
    public BossState currentState = BossState.PhaseOne;

    [Header("Phase 1")]
    [SerializeField] private WeakPoint[] weakPoints; // Lista de puntos débiles

    [Header("Phase 2")]
    [SerializeField] private GameObject minionsPrefab; // Prefab de los enemigos a spawnear
    [SerializeField] private GameObject spawnPointsContainer;
    [SerializeField] private int enemiesToSpawn = 5; // Número de enemigos a spawnear en la segunda fase

    private int destroyedWeakPoints = 0;
    private List<Transform> spawnPoints = new List<Transform>(); // Puntos de spawn para los enemigos

    private List<GameObject> spawnedEnemies = new List<GameObject>(); // Lista de enemigos spawneados
    private bool isCheckingForEnemies = false; // Controlar si estamos revisando enemigos

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
    }

    #endregion

    #region TransitionsToOtherPhases

    public void RegisterWeakPointDestroyed()
    {
        destroyedWeakPoints++;

        // Transición a la segunda fase si se destruyen 2 puntos débiles
        if (destroyedWeakPoints >= 2 && currentState == BossState.PhaseOne)
        {
            TransitionToPhaseTwo();
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
            TransitionToPhaseThree();
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
        Debug.Log("Transición a la Fase 3");
    }

    #endregion
}
