using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 10; // Cantidad de da�o que inflige la bala

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeakPoint"))
        {
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().CanTakeDamage == true)
            {
                Debug.Log("Est� haciendo da�o" + damage);

                // Obtener el componente de salud del jefe
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().TakeDamage(damage);

                other.GetComponent<WeakPoint>().RegisterHit();
            }

        }
        else if (other.CompareTag("Enemy"))
        {
            // Da�o directo al jefe si todos los puntos d�biles est�n destruidos
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossPhases>().AreWeakPointDestroyed)
            {
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().TakeDamage(damage);
                Debug.Log("Da�o directo al jefe!");
            }
        }
    }
}
