using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    #region Components

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 220;

    [Header("Blink settings")]
    [SerializeField] private float blinkRate = 0.3f;
    [SerializeField] private GameObject bossMesh;

    //private components
    private int currentHealth;
    private bool isInvulnerable;
    private bool canTakeDamage = true;
    private bool isDead;
    private Animator anim;

    //shared components
    public bool IsInvulnerable { get => isInvulnerable; set => isInvulnerable = value; }
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

    #endregion

    #region UnityFuntions

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    #endregion

    #region BasicHealthSystem

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        if (!isInvulnerable) // Solo recibe daño si no está invulnerable
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void ActiveDamageAnimation()
    {
        if (canTakeDamage == true)
        {
            anim.SetTrigger("damage");
        }
    }

    private void Die()
    {
        isDead = true;
        CanTakeDamage = false;
        anim.SetTrigger("dead");
        Debug.Log("El boss ha muerto");
    }

    #endregion

    #region BlinkSystem

    public void StartBlink()
    {
        StartCoroutine(Blink(2));
    }

    public void StopBlink()
    {
        StopCoroutine(Blink(0));
    }

    private IEnumerator Blink(int blinkTimes)
    {
        for (int i = 0; i < blinkTimes; i++)
        {
            bossMesh.SetActive(false);

            yield return new WaitForSeconds(blinkRate);

            bossMesh.SetActive(true);

            yield return new WaitForSeconds(blinkRate);
        }
    }

    #endregion
}
