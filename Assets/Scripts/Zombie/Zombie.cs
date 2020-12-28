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


    Vector3 startPosZombie;
    float dictanceToPlayer;

    ZombieState activeState;
    enum ZombieState
    {
        STAND,
        RETURN,
        MOVE_TO_PLAYER,
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

        startPosZombie = transform.position;

        activeState = ZombieState.STAND;
    }

    private void Update()
    {
        DistanceZombie();
    }

    public void UpdateHealth(int amount)
    {
        healthZombie -= amount;
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
                case ZombieState.MOVE_TO_PLAYER:
                    DoMove();
                    break;
                case ZombieState.ATTACK:
                    DoAttack();
                    break;
                case ZombieState.RETURN:
                    DoReturn();
                    break;
            }
        }
        else
        {
            movement.OnDisable();
            movement.enabled = false;
            return;
        }
    }
    //private void ChangeState(ZombieState newState)
    //{

    //}

    private void DoStand()
    {
        if (dictanceToPlayer < moveRadius)
        {
            activeState = ZombieState.MOVE_TO_PLAYER;
            return;
        }
        movement.enabled = false;
    }

    private void DoReturn()
    {
        if (dictanceToPlayer < moveRadius)
        {
            activeState = ZombieState.MOVE_TO_PLAYER;
            return;
        }

        float distanseToStart = Vector3.Distance(transform.position, startPosZombie);
        if (distanseToStart <= 0.1f)
        {
            activeState = ZombieState.STAND;
            return;
        }

        movement.targetPos = startPosZombie;
        movement.enabled = true;
    }

    private void DoMove()
    {
        if (dictanceToPlayer < attackRadius)
        {
            activeState = ZombieState.ATTACK;
            return;
        }
        if (dictanceToPlayer > saveZone)
        {
            activeState = ZombieState.RETURN;
            return;
        }
        movement.targetPos = player.transform.position;
        movement.enabled = true;
    }
    private void DoAttack()
    {
        if (dictanceToPlayer > attackRadius)
        {
            activeState = ZombieState.MOVE_TO_PLAYER;
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

    private void DamageToPlayer()
    {
        if (dictanceToPlayer > attackRadius)
        {
            return;
        }
        player.UpdateHealth(bullDamage);
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
