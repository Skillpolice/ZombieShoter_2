using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public GameObject shootPosBullet;

    [Header("Bullet")]
    public float fireRotate; //частота стрельбы
    float nextFire; //сколько прошло времени от предыдущего выстрела


    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && nextFire <= 0)
        {
            Instantiate(bulletPrefab, shootPosBullet.transform.position, transform.rotation); //Создание пули , префаб, откуда идем выстрел и нужное вращение
            nextFire = fireRotate;
        }
        if(nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
    }
}
