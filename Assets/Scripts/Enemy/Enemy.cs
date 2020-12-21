using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Bullet bullet;
    Animator animator;
    CircleCollider2D coll2D;

    public Text enemyHealthText;
    public GameObject shootPos;

    [Header("Bullet")]
    public Bullet bulletPrefab;
    public float fireRotate;

    [Header("Enemy")]
    public int bullDamage;
    public int healthEnemy;

    private void Awake()
    {
        animator = FindObjectOfType<Animator>();
        coll2D = FindObjectOfType<CircleCollider2D>();
    }

    private void Start()
    {
        enemyHealthText.text = "Enemy: " + healthEnemy.ToString();

        StartCoroutine(EnemyFire(fireRotate));
    }
    IEnumerator EnemyFire(float fire)
    {
        for (int i = 0; i < healthEnemy; i++)
        {
            yield return new WaitForSeconds(fire);
            Instantiate(bulletPrefab, shootPos.transform.position, transform.rotation);
            animator.SetTrigger("Attack");
        }
    }

    public void HealthEnemy()
    {
        healthEnemy -= bullDamage;
        enemyHealthText.text = "Enemy: " + healthEnemy.ToString();
        if(healthEnemy <= 50)
        {
            enemyHealthText.color = Color.red;
        }
        else if (healthEnemy <= 0)
        {
            enemyHealthText.text = "Enemy: Dead";
            animator.SetTrigger("Death");
            coll2D.enabled = false;
        }

    }
}
