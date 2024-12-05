using UnityEngine;

public class DamageTest : MonoBehaviour
{
    public int damage = 10; // Cantidad de daño que inflige la bala

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeakPoint"))
        {
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().CanTakeDamage == true)
            {
                Debug.Log("Está haciendo daño" + damage);

                // Obtener el componente de salud del jefe
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().TakeDamage(damage);
                GameObject.FindGameObjectWithTag("WeakPoint").GetComponent<WeakPoint>().RegisterHit();

                
            }

        }
        else
        {
            Debug.Log("Impacto en una parte no vulnerable");
        }
    }
}
