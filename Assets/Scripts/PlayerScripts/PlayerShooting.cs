using SmallHedge.SoundManager;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject gun;  // Referencia a la pistola en la mano izquierda
    [SerializeField] private Transform shootPoint;//Referencia al punto de disparo izquierdo
    //[SerializeField] private Transform cameraTransform;

    //private ObjectPool objectPool;
    [SerializeField] private float shootForce;
    [SerializeField] private GameObject bullet;

    //private void Awake()
    //{
    //    objectPool = GetComponent<ObjectPool>();
    //}

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Shooting();
            SoundManager.PlaySound(SoundType.SHOOT, volume: 0.1f);
        }
    }

    private void Shooting()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation)
             .GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce);

        //GameObject bullet = objectPool.GetGameObject();

        //// Coloca la bala en el punto de disparo
        //bullet.transform.position = shootPoint.position;

        //// Coloca la bala en el punto de disparo
        //bullet.transform.position = shootPoint.position;

        //// La dirección del disparo es el "forward" del shootPoint (dirección en la que apunta el cañón)
        //Vector3 shootDirection = shootPoint.forward;

        //// Asegúrate de que la bala se oriente en la dirección correcta
        //bullet.transform.rotation = Quaternion.LookRotation(shootDirection);

        //// Aplica la fuerza de disparo en esa dirección
        //bullet.GetComponent<Rigidbody>().AddForce(shootDirection * shootForce);
    }
}
