using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombact : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttacktime = 0f;

    void Update()
    {
        if (Time.time >= nextAttacktime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Attack();
                        nextAttacktime = Time.time + 1f/attackRate;
                    }   
        }
        
    }

    private void Attack()
    {
        // paly attack animation
        animator.SetTrigger("Attack");
        // detect enimeis in range of the attack
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit the enemy");
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
