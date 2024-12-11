using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    public int damage = 10; // Cantidad de daño que inflige la bala
    //[SerializeField] private float activeTime;

    /// <summary>
    /// when the game object set active = true
    /// </summary>
    //private void OnEnable()
    //{
    //    Debug.Log("on enable Bullet");
    //    StartCoroutine(DeactiveAfterTime());
    //}

    //private IEnumerator DeactiveAfterTime()
    //{
    //    Debug.Log("Deactive corrutine");
    //    yield return new WaitForSeconds(activeTime);
    //    gameObject.SetActive(false);
    //}


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

                //gameObject.SetActive(false);
            }

        }
        else if (other.CompareTag("Enemy"))
        {
            // Daño directo al jefe si todos los puntos débiles están destruidos
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossPhases>().AreWeakPointDestroyed)
            {
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().TakeDamage(damage);
                Debug.Log("Daño directo al jefe!");

                //gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("Minion"))
        {
            GameObject.FindGameObjectWithTag("Minion").GetComponent<MinionController>().DamageEnemy(damage);

            //gameObject.SetActive(false);
        }
    }
}
