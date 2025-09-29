using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IHealth {

    [Header("Health Settings")]
    [Tooltip("Current enemy health.")]
    [SerializeField] private int health = 100;
    [Tooltip("Enemy max health.")]
    [SerializeField] private int maxHealth = 100;

    [Header("Required components")]
    private IEnemyMovement enemyMovement;
    private HealthManager healthBar;
    private EnemyScorePoints enemyScore;
    
    private void Start() {
        enemyMovement   = GetComponent<IEnemyMovement>();
        healthBar       = GetComponentInChildren<HealthManager>();
        enemyScore      = GetComponent<EnemyScorePoints>();

        if(enemyMovement == null)   Debug.Log("EnemyHealth: EnemyMovement component is null.");
        if(healthBar == null)       Debug.Log("EnemyHealth: HealthManager component is null.");
        if(enemyScore == null)      Debug.Log("EnemyHealth: EnemyScorePoints component is null.");
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("PlayerProjectile") && !enemyMovement.isFrozen()) {
            handleTakeDamage(collision);
        }
    }

    private void handleTakeDamage(Collision projectile) {
        ProjectileDamage damage = projectile.gameObject.GetComponent<ProjectileDamage>();
        takeDamaged(damage.getDamage());
        updateHealthBar(damage.getDamage());

        if (isDead()) destroyEnemy();
        
    }

    public void takeDamaged(int damage) {
        enemyMovement.freeze();
        health -= damage;
    }
    public void addHealth(int health) {
        int newHealth = this.health + health;
        this.health = (newHealth > maxHealth) ? maxHealth : newHealth;
    }
    public int getHealth() { return health; }
    public int getMaxHealth() { return health; }
    public bool isDead() { return (health < 1) ? true : false; }
    public void destroyEnemy() {
        restoreFullHealth();
        gameObject.SetActive(false);
        GameManager.scorePoints(this.getScorePoints());
    }

    public void updateHealthBar(int damage) {
        healthBar.takeDamage(damage);
    }
    public void restoreFullHealth() {
        health = maxHealth;
        healthBar.restoreFullBar();
    }
    public int getScorePoints() {
        return enemyScore.getScorePoints();
    }
}
