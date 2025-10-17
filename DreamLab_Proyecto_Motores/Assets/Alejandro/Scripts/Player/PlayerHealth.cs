using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth {
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;

    //COMPONENTS
    private PlayerMovement playerMovement;
    private HealthManager playerHealthBar;

    //STATES
    private bool vulnerable = true;
    private bool dead = false;

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.Log("PlayerHealth: PlayerController is null.");
        }
        playerHealthBar = GetComponentInChildren<HealthManager>();
        if (playerHealthBar == null) 
        {
            Debug.Log("PlayerHealth: HealthBar is null.");
        }       
    }


    public void updateHealthBar(int damage) {
        playerHealthBar.takeDamage(damage);
    }

    public int getHealth() { return health; }
    public void addHealth(int health) {
        int newHealth = this.health + health;
        this.health = (newHealth > maxHealth) ? maxHealth : newHealth;
        playerHealthBar.heal(newHealth);
    }

    public void takeDamaged(int damage) {
        health -= damage;
        updateHealthBar(damage);
        if (health < 1) dead = true;
        vulnerable = false;
        Invoke("makeVulnerable", 1f);
    }

    public void makeVulnerable() { 
        this.vulnerable = true;
    }

    public void gameOver() { GameManager.gameOver(); }
    public bool isDead() { return dead; }
    public int getMaxHealth() { return maxHealth; }
}