using UnityEngine;

public class TakeDamageMinionTest : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            // Obtener el componente de salud del jefe
            other.GetComponent<MinionController>().DamageEnemy(damage);
        }
    }
}
