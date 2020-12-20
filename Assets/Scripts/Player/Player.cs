using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    public Bullet bulletPrefab;
    public GameObject shootPosBullet;

    [Header("Bullet")]
    public float fireRotate; //частота стрельбы
    float nextFire; //сколько прошло времени от предыдущего выстрела

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckFire();

    }

    private void CheckFire()
    {
        if (Input.GetButtonDown("Fire1") && nextFire <= 0)
        {
            Attack();
        }
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        Instantiate(bulletPrefab, shootPosBullet.transform.position, transform.rotation); //Создание пули , префаб, откуда идем выстрел и нужное вращение
        nextFire = fireRotate;
        animator.SetTrigger("Attack");
    }
}
