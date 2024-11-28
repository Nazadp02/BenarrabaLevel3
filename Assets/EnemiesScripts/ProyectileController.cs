using UnityEngine;
using System.Collections;

public class ProyectileController : MonoBehaviour
{
    [Header("Proyectile Info")]
    [SerializeField] private float activeTime = 5;
    [SerializeField] private float proyectileSpeed = 10f;
    [SerializeField] private int damage = 5;

    private Rigidbody rb;

    public int Damage { get => damage; set => damage = value; }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }






    //When the gameobject SetActive = true
    private void OnEnable()
    {
        StartCoroutine(DeactiveAfterTime());
    }

    private IEnumerator DeactiveAfterTime()
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }

    //when the bullet collide with something 
    private void OnTriggerEnter(Collider other)
    {
        //Desactive the bullet
        gameObject.SetActive(false);
    }
}
