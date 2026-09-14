using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public int attackDamage = 1;
    public float attackCooldown = 0.3f;
    public LayerMask enemyLayers;

    private float cooldownTimer;
    private Animator anim;

    void Awake() { anim = GetComponent<Animator>(); }

    void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.J) && cooldownTimer <= 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        cooldownTimer = attackCooldown;
        if (anim) anim.SetTrigger("Attack");

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var hit in hits)
        {
            hit.GetComponent<EnemyBase>()?.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}