using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour, ILauncher {
    [Header("Projectile prefab")]
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float launchSpeed = 20f;
    [SerializeField] private float cooldownSeconds = 1f;
    [SerializeField] private Pool projectilePool;

    // GONZA
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireClip;
    // GONZA

    private bool launcherAvailable = true;

    void Update() {
        if (Input.GetMouseButtonDown(0) && launcherAvailable) {
            FireProjectile();
        }
    }

    public Vector3 getCameraCenterTarget() {
        Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        Vector3 targetPoint;

        // Cast a ray from the camera to the mouse position
        if (Physics.Raycast(ray, out hit)) {
            targetPoint = hit.point; // If the ray hits an object, use that point
        } else {
            // If the ray doesn't hit anything, project a point a certain distance in front of the camera
            targetPoint = ray.GetPoint(100f); // Adjust the distance as needed
        }

        // Calculate the direction from the fire point to the target point
        Vector3 dir = (targetPoint - launchPoint.position).normalized;
        Debug.DrawRay(launchPoint.position, dir * 20f, Color.red, 2f);
        return dir;
        /*
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.AddForce(direction * launchForce, ForceMode.Impulse);
        }
        */
    }

    public void FireProjectile() {
        Debug.Log("FireProjectile ejecutado");
        GameObject newProjectile = projectilePool.getObject();
        //ProjectilePoolManager.Instance.GetProjectile();
        newProjectile.transform.position = launchPoint.position;

        //Calcula la dirección hacia donde apunta el mouse
        Vector3 launchDirection = launchPoint.forward;
        //Orienta al proyectil hacia la dirección del mouse
        newProjectile.transform.rotation = Quaternion.LookRotation(launchDirection);
        ProjectileController projectileController = newProjectile.GetComponent<ProjectileController>();

        if (projectileController != null) {
            projectileController.setLauchForce(launchSpeed);
            projectileController.Launch(launchDirection);
        }

        // GONZA
        if (audioSource != null && fireClip != null)
        {
            audioSource.PlayOneShot(fireClip);
        }
        // GONZA

        launcherAvailable = false;
        StartCoroutine(restartLaucher());
    }

    private IEnumerator restartLaucher() {
        yield return new WaitForSeconds(cooldownSeconds);
        launcherAvailable = true;
    }
}
