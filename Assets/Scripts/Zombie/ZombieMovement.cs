using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Player player;
    Zombie zombie;

    public float speedZombie = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        zombie = FindObjectOfType<Zombie>();
    }
    private void Update()
    {
        if(zombie.healthZombie > 0)
        {
            Move();
            Rotate();
        }
        else
        {
            return;
        }
    }

    public void Move()
    {
        Vector3 zombiePos = transform.position;
        Vector3 playerPos = player.transform.position;
        Vector3 direction = playerPos - zombiePos;

        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        animator.SetFloat("Walk", direction.magnitude);
        rb.velocity = direction * speedZombie;

    }

    public void Rotate()
    {
        Vector3 zombiePos = transform.position;
        Vector3 playerPos = player.transform.position;
        Vector3 direction = playerPos - zombiePos;

        direction.z = 0;
        transform.up = -direction;
    }
    public void OnDisable() //Вызывается когда включается обьект
    {
        rb.velocity = Vector2.zero;
    }


}
