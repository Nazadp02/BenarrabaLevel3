using UnityEngine;

public class BulletDamage : MonoBehaviour
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

                other.GetComponent<WeakPoint>().RegisterHit();
            }

        }
        else if (other.CompareTag("Enemy"))
        {
            // Daño directo al jefe si todos los puntos débiles están destruidos
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossPhases>().AreWeakPointDestroyed)
            {
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().TakeDamage(damage);
                Debug.Log("Daño directo al jefe!");
            }
        }
    }
}
