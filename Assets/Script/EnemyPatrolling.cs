using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    [Header ("Enemy Animator")]
    [SerializeField] private Animator anim;

    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;


    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed = 2f;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("idle Behavior")]
    [SerializeField] private float idleDuration= 1f;
    private float idleTimer;


    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("isRun", false);
    }


    private void Update()
    {
        if (movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
                
            }
            else
            {
                // change direction
                DirectionChange();
            }
            
        }
        else
        {
            if(enemy.position.x  <= rightEdge.position.x)
            {
                MoveInDirection(1);
                
            }
            else
            {
                // change direction
                DirectionChange();
            }
           
            

        }


    }
    private void MoveInDirection(int _direction)
        
    {
        idleTimer = 0;

        // make an animation active 
        anim.SetBool("isRun", true);

        

        // make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        // move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);  
    }

 

    private void DirectionChange()
    {
        anim.SetBool("isRun", false);

        // idle behavior after comming to the edge point
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
               


        

    }
}
