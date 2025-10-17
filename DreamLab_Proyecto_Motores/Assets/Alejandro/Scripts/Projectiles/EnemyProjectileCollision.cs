using UnityEngine;

public class EnemyProjectileCollision : MonoBehaviour
{
    private ProjectileDamage projectileDamage;

    private void Awake()
    {
        // Busca el componente que define el daño
        projectileDamage = GetComponent<ProjectileDamage>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // Si impacta contra algo que tiene salud
        IHealth targetHealth = other.gameObject.GetComponent<IHealth>();

        if (targetHealth != null && projectileDamage != null)
        {
            // Usa el daño del otro script, sin redefinirlo acá
            
            PlayerHealth playerHealt = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealt != null && projectileDamage != null)
            {
                int damageValue = projectileDamage.getDamage();
                targetHealth.takeDamaged(damageValue);
                Debug.Log($"Impacto: {other.gameObject.name} recibe {damageValue} de daño");
                
            }
            
        }
    }
}
