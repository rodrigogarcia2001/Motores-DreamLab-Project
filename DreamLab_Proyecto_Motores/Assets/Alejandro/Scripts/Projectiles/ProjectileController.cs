using System;
using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    [Header("Launch force for projectile.")]
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float timeToDestroy = 1.5f;

    //COMPONENTS
    private Rigidbody projectileRb;
    void Awake() {
        projectileRb = GetComponent<Rigidbody>();
        if (projectileRb == null) Debug.Log("ProjectileController: RigidBody is null.");
    }

    public void Launch(Vector3 direction) {
        projectileRb.AddForce(direction.normalized * launchForce, ForceMode.Impulse);
        StartCoroutine(destroyProjectile());
    }

    void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            destroy();
        }   
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
