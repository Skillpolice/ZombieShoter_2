using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Player player;
    Enemy enemy;
    Zombie zombie;

    public float bullSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
        zombie = FindObjectOfType<Zombie>();

        rb.velocity = -transform.up * bullSpeed; //Скорость пули - стреляет куда смотрит
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    player.HealthPlayer();
        //    Destroy(gameObject);
        //}
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    enemy.HealthEnemy();
        //    Destroy(gameObject);
        //}
        if (collision.gameObject.CompareTag("Zombie"))
        {
            zombie.UpdateHealth();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible() //Уничтожение обьектов за пределы камеры
    {
        Destroy(gameObject);
    }
}
