using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

    
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;


    [SerializeField] GameObject healthBarCanvas;
    [SerializeField] FloatingHealthBar healthBar;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // play damage animation
        animator.SetTrigger("Hurt");

        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
            Destroy(healthBarCanvas);
        }
    }

    void Die()
    {
        Debug.Log("Ennemy Died");
        // die animation

        
        animator.SetBool("isDie", true);

        GetComponent<Rigidbody2D>().bodyType= RigidbodyType2D.Static;
        /// GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

    }
}