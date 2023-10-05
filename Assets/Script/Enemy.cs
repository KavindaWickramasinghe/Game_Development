using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    private Rigidbody2D rb;


    [SerializeField] FloatingHealthBar healthBar;
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject enemyObject;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.updateHealthBar(currentHealth, maxHealth);
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.updateHealthBar(currentHealth, maxHealth);
        // play damage animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
            Destroy(HealthBar);
            
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    void Die()
    {
        Debug.Log("Ennemy Died");
        // die animation

        
        animator.SetTrigger("die");

        GetComponent<Rigidbody2D>().bodyType= RigidbodyType2D.Static;
        /// GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }

    public void RemoveDiedEnemy()
    {
        Destroy(enemyObject);
    }
}   

