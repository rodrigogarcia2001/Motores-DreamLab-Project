using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField]
    [Tooltip("Projectile current damage")]
    private int damage = 25;

    public int getDamage() { return damage; }
}
