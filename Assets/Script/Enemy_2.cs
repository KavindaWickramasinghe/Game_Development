using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    [SerializeField] private  float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider ;
    [SerializeField] private LayerMask PlayerLayer;


    //public Transform attackPoint;

    public int attackDamage = 10;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;


        if (PlayerInSight())
        {
            if (cooldownTimer  >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                Attack();



            }


        }
        
    }

    // set enemy to attack
    private void Attack()
    {
        // paly attack animation

        // detect enimeis in range of the attack
        Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(boxCollider.bounds.center + colliderDistance * range * transform.localScale.x * transform.right,
                    new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                    0, PlayerLayer); 

        // damage them
        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Hit the player");
            player.GetComponent<PlayerLife>().TakeDamage(attackDamage);
        }
    }

    // checking enemy detect the player
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance,
            new Vector3(boxCollider.bounds.size.x*range,boxCollider.bounds.size.y,boxCollider.bounds.size.z),
            0,Vector2.left,0, PlayerLayer);
            

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
