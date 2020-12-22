using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Player player;
    Animator animator;
    ZombieMovement movement;

    public float attackRadius = 4f;
    public float moveRadius = 10f;
    public float saveZone = 17f;

    ZombieState activeState;
    enum ZombieState
    {
        STAND,
        MOVE,
        ATTACK
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        movement = FindObjectOfType<ZombieMovement>();
        activeState = ZombieState.STAND;
    }

    private void Update()
    {
       
        DistanceZombie();
    }

    public void DistanceZombie()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        switch (activeState)
        {
            case ZombieState.STAND:  
                if(distance > moveRadius)
                {
                    activeState = ZombieState.MOVE;
                    movement.Move();
                    return;
                }
                animator.SetFloat("Walk", 0);
                break;
            case ZombieState.MOVE:
                if (distance < attackRadius)
                {
                    activeState = ZombieState.ATTACK;
                    return;
                }
                animator.SetFloat("Walk", 1);
                break;
            case ZombieState.ATTACK:
                if(distance > attackRadius)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }
                animator.SetTrigger("Attack");
                break;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, saveZone);
    }
}
