using SmallHedge.SoundManager;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    #region Components

    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    private bool canTakeDamage = true;
    private bool isTakingDamage = false;
    private float timeSinceHit;
    private float invincibilityTimer = 0.3f;

    public bool IsTakingDamage { get => isTakingDamage; set => isTakingDamage = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!canTakeDamage)
        {
            if (timeSinceHit > invincibilityTimer)
            {
                canTakeDamage = true;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public void TakeDamage(int amount)
    {
        if (canTakeDamage)
        {
            //reduce health
            currentHealth -= amount;

            isTakingDamage = true;
            canTakeDamage = false;

            SoundManager.PlaySound(SoundType.PLAYERDAMAGE);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        isTakingDamage = false;
    }

    private void Die()
    {
        isTakingDamage = false;
        canTakeDamage = false;

        GameManager.Instance.EndGame(false);
    }
}
