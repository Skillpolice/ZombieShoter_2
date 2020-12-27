using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    Player player;
    Animator animator;
    ZombieMovement movement;
    CircleCollider2D coll2D;

    public Text textHealthZombie;

    [Header("Zones")]
    public float attackRadius = 4f;
    public float moveRadius = 10f;
    public float saveZone = 17f;

    [Header("Zombie")]
    public int healthZombie = 100;
    public float hitRotate;
    public int bullDamage;
    float hitNexAttack;


    bool isDead = false;
    float dictanceToPlayer;

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
        coll2D = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        movement = FindObjectOfType<ZombieMovement>();

        textHealthZombie.text = "Zombie: " + healthZombie.ToString();
        activeState = ZombieState.STAND;
    }

    private void Update()
    {
        DistanceZombie();
    }

    public void HealthZombie()
    {
        healthZombie -= player.bullDamage;
        textHealthZombie.text = "Zombie: " + healthZombie.ToString();
        if (healthZombie <= 50)
        {
            textHealthZombie.color = Color.red;
        }
        if (healthZombie <= 0)
        {
            textHealthZombie.text = "Zombie: Dead";
            animator.SetTrigger("Death");
            coll2D.enabled = false;
            return;
        }
    }


    public void DistanceZombie()
    {
        dictanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (healthZombie > 0)
        {
            switch (activeState)
            {
                case ZombieState.STAND:
                    DoStand();
                    break;
                case ZombieState.MOVE:
                    DoMove();
                    break;
                case ZombieState.ATTACK:
                    DoAttack();
                    break;
            }
        }
        else if (player.healthPlayer <= 0)
        {
            return;
        }

    }

    public void DoStand()
    {
        if (dictanceToPlayer < moveRadius)
        {
            activeState = ZombieState.MOVE;
            return;
        }
        movement.enabled = false;
    }
    public void DoMove()
    {
        if (dictanceToPlayer < attackRadius)
        {
            activeState = ZombieState.ATTACK;
            return;
        }
        if (dictanceToPlayer > saveZone)
        {
            activeState = ZombieState.STAND;
            return;
        }
        movement.enabled = true;
    }
    public void DoAttack()
    {
        if (dictanceToPlayer > attackRadius)
        {
            activeState = ZombieState.MOVE;
            return;
        }
        movement.enabled = false;

        hitNexAttack -= Time.deltaTime;
        if (hitNexAttack < 0)
        {
            animator.SetTrigger("Attack");
            hitNexAttack = hitRotate;
        }
    }

    public void DamageToPlayer()
    {
        if(dictanceToPlayer > attackRadius)
        {
            return;
        }
        player.HealthPlayer(bullDamage);
    }

    //IEnumerator AttackCoroutine()
    //{
    //    while (true)
    //    {
    //        player.HealthPlayer(bullDamage);
    //        animator.SetTrigger("Attack");
    //        yield return new WaitForSeconds(hitRotate);
    //    }
    //}

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
