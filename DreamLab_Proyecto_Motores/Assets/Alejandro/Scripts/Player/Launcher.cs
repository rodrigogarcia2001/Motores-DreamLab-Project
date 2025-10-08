using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour, ILauncher {
    [Header("Projectile prefab")]
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float launchSpeed = 20f;
    [SerializeField] private float cooldownSeconds = 1f;
    [SerializeField] private Pool projectilePool;

    private bool launcherAvailable = true;

    void Update() {
        if (Input.GetMouseButtonDown(0) && launcherAvailable) {
            FireProjectile();
        }
    }

    public Vector3 getCameraCenterTarget() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;

        // Cast a ray from the camera to the mouse position
        if (Physics.Raycast(ray, out hit)) {
            targetPoint = hit.point; // If the ray hits an object, use that point
        } else {
            // If the ray doesn't hit anything, project a point a certain distance in front of the camera
            targetPoint = ray.GetPoint(50f); // Adjust the distance as needed
        }

        // Calculate the direction from the fire point to the target point
        return (targetPoint - transform.position).normalized;
        /*
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.AddForce(direction * launchForce, ForceMode.Impulse);
        }
        */
    }

    public void FireProjectile() {
        GameObject newProjectile = projectilePool.getObject();
        //ProjectilePoolManager.Instance.GetProjectile();
        newProjectile.transform.position = launchPoint.position;
        newProjectile.transform.rotation = launchPoint.rotation;
        ProjectileController projectileController = newProjectile.GetComponent<ProjectileController>();

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
}
