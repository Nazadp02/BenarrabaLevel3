using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public int damage = 10; // Daño que se inflige al jefe
    public ParticleSystem hitEffect; // Sistema de partículas que se activará
    public BossHealth BossHealth;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es una bala
        if (other.CompareTag("Bullet"))
        {
            // Instancia el efecto de partículas
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            
            
        }
    }
}