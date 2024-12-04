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
    [SerializeField] private GameObject[] WeakPoints;

    //private components
    private int currentHealth;
    private bool isInvulnerable = false;
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
        for (int i = 0; i < blinkTimes; i++)
        {
            bossMesh.SetActive(false);

            for (int j = 0; j < WeakPoints.Length; j++)
            {
                WeakPoints[j].SetActive(false);
            }

            yield return new WaitForSeconds(blinkRate);

            bossMesh.SetActive(true);

            for (int j = 0; j < WeakPoints.Length; j++)
            {
                WeakPoints[j].SetActive(true);
            }

            yield return new WaitForSeconds(blinkRate);
        }
    }

    #endregion
}
