using UnityEngine;
using UnityEngine.UI;

public class SliderImageLogic : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = 1f;
    }
    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.CurrentHealth / playerHealth.MaxHealth;
    }
}
