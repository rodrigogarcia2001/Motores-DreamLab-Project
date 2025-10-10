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
        Debug.Log("Launch from: " + transform.position); 
        Debug.Log("Direction: " + direction);
        //reinicio la fisica antes de que se lanze, as� no se acumula de lanzamientos anteriores
        projectileRb.linearVelocity = Vector3.zero;
        projectileRb.angularVelocity = Vector3.zero;

        projectileRb.AddForce(direction.normalized * launchForce, ForceMode.Impulse);
        StartCoroutine(destroyProjectile());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Player Hit!!");
            return;
        }
        destroy();
    }
    
    void OnCollisionEnter(Collision collision) {
        Debug.Log("collision!!");
        destroy();
    }

    public float getLaunchForce() { return launchForce; }
    public void setLauchForce(float force) { launchForce = force; }
    private IEnumerator destroyProjectile() {
        yield return                        new WaitForSeconds(timeToDestroy);
        if (gameObject.activeInHierarchy)   destroy();
    }
    private void destroy() {
        Debug.Log("Destroy target: " + transform.position + " | Force: "+ projectileRb.GetAccumulatedForce());
        
        gameObject.SetActive(false);
    }

}
