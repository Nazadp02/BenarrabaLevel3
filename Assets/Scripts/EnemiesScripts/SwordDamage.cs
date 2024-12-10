using Unity.VisualScripting;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Minion"))
        {
            // Obtener el componente de salud del jefe
            other.GetComponent<MinionController>().DamageEnemy(damage);
        }
        else if (other.CompareTag("Skull"))
        {
            Destroy(other);
        }
    }
}
