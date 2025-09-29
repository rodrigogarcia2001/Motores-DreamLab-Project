using UnityEngine;

public class EnemyRangedMovement : MonoBehaviour, IEnemyMovement {
    [Header("Enemy movement settings")]
    [SerializeField] private float speed;
    [SerializeField] private float freezeCooldown = 2f;

    [Header("Required components")]
    private Rigidbody enemyRb;
    private EnemyLauncher enemyLauncher;

    //VARIABLES
    private Vector3 playerPosition;
    private Vector3 targetDirection;
    private float rotationSpeed = 200f;

    //STATES
    private bool frozen = false;

    void Start() {
        enemyRb = GetComponent<Rigidbody>();
        enemyLauncher = GetComponent<EnemyLauncher>();
        if (enemyRb == null) Debug.Log("EnemyMovement:RigidBody component is null.");
        if (enemyLauncher == null) Debug.Log("EnemyMovement: EnemyLauncher component is null.");
    }
    void Update() {
        if (!frozen) {
            findPlayerPosition();
            
        }
    }

    private void FixedUpdate() {
        lookAtPlayer();
        if(!playerOnSight())
            moveTowardPlayer();
    }

    public bool playerOnSight() {
        float distanceToTarget = Vector3.Distance(transform.position, playerPosition);

        if (distanceToTarget < enemyLauncher.getAttackRange()) return true;

        return false;
    }

    private void findPlayerPosition() {
        playerPosition = GameObject.Find("Player").transform.position;
        targetDirection = (playerPosition - transform.position);
        targetDirection.y = 0;
    }
    private void moveTowardPlayer() {
        Vector3 directionToPlayer = targetDirection.normalized;
        //Vector3 targetPosition = transform.position + directionToPlayer * speed * Time.fixedDeltaTime;
        enemyRb.AddForce(directionToPlayer * speed, ForceMode.Force);
    }

    public void lookAtPlayer() {
        if (targetDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion newRotation = Quaternion.RotateTowards(enemyRb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            enemyRb.MoveRotation(newRotation);
        }
    }

    public void freeze() {
        frozen = true;
        enemyRb.isKinematic = frozen;
        Invoke("unfreeze", freezeCooldown);
    }


    public void unfreeze() {
        frozen = false;
        enemyRb.isKinematic = frozen;
    }
    public bool isFrozen() { return frozen; }
    public float getFreezeCooldown() { return freezeCooldown; }

}
