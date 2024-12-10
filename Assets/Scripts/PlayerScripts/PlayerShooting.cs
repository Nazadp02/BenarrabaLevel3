using Oculus.Interaction;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject gunRight; // Referencia a la pistola en la mano derecha
    [SerializeField] private Transform shootPointRight;//Referencia al punto de disparo derecho


    [SerializeField] private GameObject gunLeft;  // Referencia a la pistola en la mano izquierda
    [SerializeField] private Transform shootPointLeft;//Referencia al punto de disparo izquierdo



    [SerializeField] private GameObject Bullet;
    [SerializeField] private float shootForce;



    private void Update()
    {


        // Detectar si se presiona el botón de disparo del controlador derecho
        if (gunLeft.gameObject.CompareTag("GunLeft") && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                Shooting(gunLeft, shootPointLeft);
            } 
        }

        // Detectar si se presiona el botón de disparo del controlador izquierdo
        if (gunRight.gameObject.CompareTag("GunRight") && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                Shooting(gunRight, shootPointRight);
            }
        }
    }

    private void Shooting(GameObject gun, Transform shootPoint)
    {
       Instantiate(Bullet, shootPoint.position, shootPoint.rotation)
            .GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce);
    }
}
