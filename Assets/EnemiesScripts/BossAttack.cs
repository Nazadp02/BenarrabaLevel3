using UnityEngine;

public class BossAttack : MonoBehaviour
{

    #region Components

    //initial properties
    private Animator Animator;

    //shoot properties 
    [Header("Attack Settings")]
    [SerializeField] private float attackRate = 0.2f;
    [SerializeField] private Transform proyectileShootPoint;
    [SerializeField] private GameObject proyectile;

    private float lastAttackTime;

    //booleans
    private bool isShootingProyectile;

    #endregion

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private bool CanAttack()
    {
        if (Time.time - lastAttackTime >= attackRate)
        {
            return true; 
        }

        return false;
    }

    private void ProjectileAttack()
    {
        lastAttackTime = Time.time;

    }
}
