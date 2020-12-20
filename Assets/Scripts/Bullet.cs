using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bullSpeed;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = -transform.up * bullSpeed; //Скорость пули - стреляет куда смотрит
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
