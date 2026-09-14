using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public float invulnerabilityTime = 1f;
    public Transform respawnPoint;

    public event Action<int, int> OnHealthChanged; // (actual, max)
    public event Action OnDeath;

    private float invulTimer;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (invulTimer > 0)
        {
            invulTimer -= Time.deltaTime;
            // Parpadeo visual
            if (sr) sr.enabled = Mathf.FloorToInt(invulTimer * 10f) % 2 == 0;
        }
        else if (sr) sr.enabled = true;
    }

    public void TakeDamage(int amount)
    {
        if (invulTimer > 0) return;
        currentHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        invulTimer = invulnerabilityTime;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void Die()
    {
        OnDeath?.Invoke();
        // Respawn en checkpoint
        if (respawnPoint != null)
            transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Damage"))
            TakeDamage(1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
            TakeDamage(1);
        else if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform;
            other.GetComponent<Checkpoint>()?.Activate();
        }
        else if (other.CompareTag("Collectible"))
        {
            other.GetComponent<Collectible>()?.Collect();
        }
    }
}