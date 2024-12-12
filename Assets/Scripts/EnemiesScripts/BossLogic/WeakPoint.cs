using SmallHedge.SoundManager;
using UnityEngine;
using UnityEngine.UI;

public class WeakPoint : MonoBehaviour
{
    #region Components

    [Header("Weak Points settings")]
    [SerializeField] private int maxHits = 3;
    [SerializeField] private Image imageWeakPoint;
    [SerializeField] private Collider weakPoint;

    private int currentHits = 0;

    #endregion

    #region UnityFuntions

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossAttack>().IsMoving == true || 
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().IsInvulnerable == true)
        {
            DesactiveWeakPoint();
        }
        else
        {
            ActiveWeakPoint();
        }
    }

    #endregion

    #region WeakPointSystem

    public void RegisterHit()
    {
        currentHits++;

        if (currentHits >= maxHits)
        {
            SoundManager.PlaySound(SoundType.WEAKPOINTDESTRUCTION);
            DestroyWeakPoint();
        }
    }

    private void DestroyWeakPoint()
    {
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossHealth>().ActiveDamageAnimation();
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossPhases>().RegisterWeakPointDestroyed();

        Destroy(imageWeakPoint);
        Destroy(this.gameObject);
    }

    #endregion

    #region ShowWeakPoint

    public void DesactiveWeakPoint()
    {
        if (weakPoint != null)
        {
            imageWeakPoint.enabled = false;
            weakPoint.enabled = false;
        }
    }

    public void ActiveWeakPoint()
    {
        if (weakPoint != null)
        {
            imageWeakPoint.enabled = true;
            weakPoint.enabled = true;
        }
    }

    #endregion
}
