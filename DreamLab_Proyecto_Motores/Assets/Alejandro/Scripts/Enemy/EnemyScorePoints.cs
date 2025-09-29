using UnityEngine;

public class EnemyScorePoints : MonoBehaviour
{
    [Header("Score Points Settings")]
    [SerializeField]
    [Tooltip("Points for killing enemy.")]
    private int scorePoints = 50;

    public int getScorePoints() { return scorePoints; }
}
