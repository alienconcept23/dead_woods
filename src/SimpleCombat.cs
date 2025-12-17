using UnityEngine;

[RequireComponent(typeof(Health))]
public class SimpleCombat : MonoBehaviour
{
    public int meleeDamage = 25;
    public float attackRange = 1.5f;

    public void MeleeAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var c in hits)
        {
            if (c.gameObject == gameObject) continue;
            var h = c.GetComponent<Health>();
            if (h != null)
            {
                h.TakeDamage(meleeDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}