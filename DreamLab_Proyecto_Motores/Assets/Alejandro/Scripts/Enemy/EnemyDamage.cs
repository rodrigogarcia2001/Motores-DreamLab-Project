using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {
    [Header("Attack Settings")]
    [SerializeField]
    [Tooltip("Playercurrent damage")]
    private int damage = 50;
    public int getDamage() { return damage; }
}
