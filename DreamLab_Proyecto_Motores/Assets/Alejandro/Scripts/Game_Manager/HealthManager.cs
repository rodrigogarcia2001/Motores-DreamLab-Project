using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour, IHealthBar {
    [Header("Required Components")]
    [Tooltip("Image to use as a fill bar.")]
    [SerializeField] private Image healthBar;
    
    //VARIABLES
    private float healthAmount = 100f;
    private float maxHealth = 0;

    // REQUIRED COMPONENTS
    private IHealth healthComponent;
    
    private void Start() {
        healthComponent = GetComponentInParent<IHealth>();
        if (healthComponent == null) Debug.Log("HealthManager: HealthComponent missing.");
        else {
            healthAmount = healthComponent.getHealth();
            maxHealth = healthComponent.getMaxHealth();
        }
    }
    public void takeDamage(float damage) {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / maxHealth;
    }

    public void heal(float healingAmount) {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);

        healthBar.fillAmount = healthAmount /maxHealth;
    }

    public void restoreFullBar() {
        healthAmount = maxHealth;
        healthBar.fillAmount = 1f;
    }

    private void Update() {
        transform.LookAt(Camera.main.transform.position);
    }
}
