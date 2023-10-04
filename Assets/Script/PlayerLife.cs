using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] FloatingHealthBar healthBar;
    [SerializeField] GameObject HealthBar;
    int currentHealth;

    private Animator anim;
    private Rigidbody2D rb;


    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    private void Start()
    {
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.updateHealthBar(currentHealth, maxHealth);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.updateHealthBar(currentHealth, maxHealth);
        // play damage animation
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
            Destroy(HealthBar);
            ReastartLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps")) 
        {

            Die();
            ReastartLevel();


        }
    }





    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }
    private void ReastartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
