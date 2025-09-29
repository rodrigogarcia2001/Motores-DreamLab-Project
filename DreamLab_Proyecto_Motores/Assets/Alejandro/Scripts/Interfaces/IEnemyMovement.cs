using UnityEngine;

public interface IEnemyMovement {
    public void lookAtPlayer();
    public void freeze();
    public void unfreeze();
    public bool isFrozen();
    public float getFreezeCooldown();
}
