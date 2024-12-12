using SmallHedge.SoundManager;
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
    private bool areMinionsDeath;
    private Animator anim;

    //shared components
    public bool IsInvulnerable { get => isInvulnerable; set => isInvulnerable = value; }
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

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

            SoundManager.PlaySound(SoundType.ENEMYHIT, volume: 0.2f);

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

        GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");

        foreach (GameObject minion in minions)
        {
            Destroy(minion);
            areMinionsDeath = true;
        }

        anim.SetTrigger("dead");
        SoundManager.PlaySound(SoundType.BOSSDEATH);
        Debug.Log("El boss ha muerto");
    }

    public void OnDeathAnimationEnd()
    {
        GameManager.Instance.EndGame(true); // Llama al metodo de victoria
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
