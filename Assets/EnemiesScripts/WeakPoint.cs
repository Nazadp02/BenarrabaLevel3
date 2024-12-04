using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public int damage = 10; // Da�o que se inflige al jefe
    public ParticleSystem hitEffect; // Sistema de part�culas que se activar�
    public BossHealth BossHealth;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es una bala
        if (other.CompareTag("Bullet"))
        {
            // Instancia el efecto de part�culas
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            
            
        }
    }
}