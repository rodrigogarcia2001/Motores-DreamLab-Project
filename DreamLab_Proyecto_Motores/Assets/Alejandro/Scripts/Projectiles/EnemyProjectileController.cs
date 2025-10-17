using System.Collections;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour {
    [Header("Launch force for projectile.")]
    [SerializeField] private float launchForce = 5f;
    [SerializeField] private float timeToDestroy = 1.5f;

    //COMPONENTS
    private Rigidbody projectileRb;
    void Awake() {
        projectileRb = GetComponent<Rigidbody>();
        if (projectileRb == null) Debug.Log("EnemyProjectileController: RigidBody is null.");
    }

    public void Launch(Vector3 direction) {

        // Reset de velocidad y rotaci�n

        projectileRb.linearVelocity = Vector3.zero;
        projectileRb.angularVelocity = Vector3.zero;

        projectileRb.linearVelocity = direction.normalized * launchForce;
        StartCoroutine(destroyProjectile());
    }

    private void FixedUpdate()
    {
        // Rotar hacia la direcci�n de movimiento
        if (projectileRb.linearVelocity.sqrMagnitude > 0.0001f)
            projectileRb.rotation = Quaternion.LookRotation(projectileRb.linearVelocity);
    }

    public float getLaunchForce() { return launchForce; }
    public void setLauchForce(float force) { launchForce = force; }
    private IEnumerator destroyProjectile() {
        yield return new WaitForSeconds(timeToDestroy);
        destroy();
    }
    private void destroy() {
        gameObject.SetActive(false);
    }
}
