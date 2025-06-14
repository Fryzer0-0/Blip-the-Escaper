using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.0f;

    private Health playerHealth;
    private float lastAttackTime;

    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        lastAttackTime = Time.time - attackCooldown; // Allow immediate attack
    }

    void Update()
    {
        if (playerHealth != null && Vector3.Distance(transform.position, playerHealth.transform.position) <= attackRange)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    private void Attack()
    {
        playerHealth.TakeDamage(damage);
        Debug.Log($"Enemy attacked! Player took {damage} damage.");
    }
}
