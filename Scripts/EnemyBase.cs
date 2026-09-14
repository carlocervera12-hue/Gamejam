using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float health = 100f;

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Enemigo recibió daño. Vida restante: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}