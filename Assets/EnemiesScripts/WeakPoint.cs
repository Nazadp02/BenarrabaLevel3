using UnityEngine;
using UnityEngine.UI;

public class WeakPoint : MonoBehaviour
{
    [Header("Weak Points settings")]
    [SerializeField] private int maxHits = 3;
    [SerializeField] private Image imageWeakPoint;
    [SerializeField] private GameObject weakPoint;

    private int currentHits = 0;

    private void Update()
    {
        DesactiveWeakPoint();    
    }

    public void RegisterHit()
    {
        currentHits++;
        

        if (currentHits >= maxHits)
        {
            DestroyWeakPoint();
        }
    }

    private void DestroyWeakPoint()
    {
        imageWeakPoint.enabled = false;
        weakPoint.SetActive(false);

        GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().ActiveDamageAnimation();

    }

    private void DesactiveWeakPoint()
    {
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossAttack>().IsMoving == true)
        {
            imageWeakPoint.enabled = false;
            weakPoint.SetActive(false);
        }
    
    }
}
