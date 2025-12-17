using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
public class EnemyAI : MonoBehaviour
{
    public float wanderRadius = 5f;
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 1.2f;

    private Transform _player;
    private float _lastAttack = -10f;
    private Health _health;

    void Awake()
    {
        _health = GetComponent<Health>();
    }

    void Start()
    {
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj) _player = playerObj.transform;
    }

    void Update()
    {
        if (_player == null) return;

        float dist = Vector3.Distance(transform.position, _player.position);
        if (dist < attackRange)
        {
            TryAttack();
        }
        else if (dist < detectionRange)
        {
            // Move towards player (simple)
            transform.position = Vector3.MoveTowards(transform.position, _player.position, Time.deltaTime * 2f);
        }
        else
        {
            // Simple wander
            transform.Translate(Vector3.forward * Mathf.Sin(Time.time) * Time.deltaTime * 0.5f);
        }
    }

    void TryAttack()
    {
        if (Time.time - _lastAttack < attackCooldown) return;
        _lastAttack = Time.time;

        var playerHealth = _player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}