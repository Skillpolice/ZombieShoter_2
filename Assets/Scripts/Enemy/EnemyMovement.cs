using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        EnemyRotate();
    }

    public void EnemyRotate()
    {
        Vector3 enemyPos = transform.position;
        Vector3 playerPos = playerMovement.transform.position;
        Vector3 direction = enemyPos - playerPos;

        direction.z = 0;
        transform.up = direction;

    }
}
