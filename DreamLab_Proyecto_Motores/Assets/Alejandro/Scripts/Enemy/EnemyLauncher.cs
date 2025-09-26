using System.Collections;
using UnityEngine;

public class EnemyLauncher : MonoBehaviour , ILauncher {
    [Header("Projectile prefab")]
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float launchSpeed = 20f;
    [SerializeField] private float cooldownSeconds = 1f;
    [SerializeField] private Pool projectilePool;
    [SerializeField] private float attackRange = 6f;

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
        GameObject newProjectile = projectilePool.getObject();//ProjectilePoolManager.Instance.GetProjectile();
        newProjectile.transform.position = launchPoint.position;
        newProjectile.transform.rotation = Quaternion.Euler(new Vector3(0f,launchPoint.rotation.y,0f));//launchPoint.rotation;

        EnemyProjectileController projectileController = newProjectile.GetComponent<EnemyProjectileController>();

        if (projectileController != null) {
            Vector3 launchDirection = launchPoint.forward;
            projectileController.setLauchForce(launchSpeed);
            projectileController.Launch(launchDirection);
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
