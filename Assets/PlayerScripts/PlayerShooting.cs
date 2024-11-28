using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject gunRight; // Referencia a la pistola en la mano derecha
    [SerializeField] private Transform shootPointRight;//Referencia al punto de disparo derecho
    [SerializeField] private Animator gunAnimatorRight;

    [SerializeField] private GameObject gunLeft;  // Referencia a la pistola en la mano izquierda
    [SerializeField] private Transform shootPointLeft;//Referencia al punto de disparo izquierdo
    [SerializeField] private Animator gunAnimatorLeft;


    [SerializeField] private GameObject Bullet;
    [SerializeField] private float shootForce;



    private void Update()
    {
        // Detectar si se presiona el botón de disparo del controlador derecho
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                gunAnimatorLeft.SetBool("IsShooting", true);

                Shooting(gunLeft, shootPointLeft);

                gunAnimatorLeft.SetBool("IsShooting", false);

            }
            
        }

        // Detectar si se presiona el botón de disparo del controlador izquierdo
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                gunAnimatorRight.SetBool("IsShooting", true);

                Shooting(gunRight, shootPointRight);

                gunAnimatorRight.SetBool("IsShooting", false);

            }

        }
    }

    private void Shooting(GameObject gun, Transform shootPoint)
    {

       Instantiate(Bullet, shootPoint.position, shootPoint.rotation)
            .GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce);



    }
}
