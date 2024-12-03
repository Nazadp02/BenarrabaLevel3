using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    #region Components

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;

    [Header("Blink")]
    [SerializeField] private float blinkRate = 0.3f;
    [SerializeField] private GameObject bossMesh;

    //private components
    private int currentHealth;
    private bool isInvulnerable = false;
    private bool canTakeDamage = false;
    private Animator anim;

    public bool IsInvulnerable { get => isInvulnerable; set => isInvulnerable = value; }

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
        if (!IsInvulnerable) // Solo recibe daño si no está invulnerable
        {
            currentHealth -= damage;
            anim.SetBool("dead", true);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        //TODO die System
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
        canTakeDamage = false;

        for (int i = 0; i < blinkTimes; i++)
        {
            // Cambiar la emisión al color deseado (blanco en este caso)
            bossMesh.SetActive(false);
            yield return new WaitForSeconds(blinkRate);

            // Volver a color de emisión normal (sin emisión)
            bossMesh.SetActive(true);
            yield return new WaitForSeconds(blinkRate);
        }

        canTakeDamage = true;
    }

    #endregion
}
