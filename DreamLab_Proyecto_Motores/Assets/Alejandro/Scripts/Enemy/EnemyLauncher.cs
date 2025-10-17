using System.Collections;
using UnityEngine;

public class EnemyLauncher : MonoBehaviour , ILauncher {
    [Header("Player Reference")]
    [SerializeField] private Transform player;

    [Header("Projectile prefab")]
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float launchSpeed = 20f;
    [SerializeField] private float cooldownSeconds = 1f;
    [SerializeField] private Pool projectilePool;
    [SerializeField] private float attackRange = 6f;

    private Rigidbody rb;
    private EnemyRangedMovement enemyMovement;
    private bool launcherAvailable = true;

    private void Start() {
        enemyMovement = GetComponent<EnemyRangedMovement>();
        if (enemyMovement == null) Debug.Log("EnemyLauncher: EnemyRangedMovement is null");
    }

    void Update() {
        if (enemyMovement.playerOnSight() && launcherAvailable) {
            FireProjectile();
        }
    }

    public void FireProjectile() {
        GameObject newProjectile = projectilePool.getObject();
        //ProjectilePoolManager.Instance.GetProjectile();
        newProjectile.transform.position = launchPoint.position;

        Vector3 directionToPlayer = (player.position - launchPoint.position).normalized;     
        newProjectile.transform.rotation = Quaternion.LookRotation(directionToPlayer);

        EnemyProjectileController projectileController = newProjectile.GetComponent<EnemyProjectileController>();
        if (projectileController != null) {
            projectileController.setLauchForce(launchSpeed);
            projectileController.Launch(directionToPlayer);
        }
        launcherAvailable = false;
        StartCoroutine(restartLaucher());
    }

    private IEnumerator restartLaucher() {
        yield return new WaitForSeconds(cooldownSeconds);
        launcherAvailable = true;
    }

    public float getAttackRange() {  return attackRange; }
}
