using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float movementSpeed;
    private int killReward;
    private int attackDamage;
    private GameObject targetTile;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTile == null)
        {
            initializeEnnemy();
        }
        checkPosition();
        moveEnnemy();
    }

    private void moveEnnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, movementSpeed * Time.deltaTime);
    }

    private void checkPosition()
    {
        if (targetTile != null || targetTile != Map.EndTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;

            if (distance < 0.01f)
            {
                int currentIndex = Map.mapTiles.IndexOf(targetTile);
                Debug.Log("cahgement");

                targetTile = Map.mapTiles[currentIndex + 1];
            }
        }
    }

    private void initializeEnnemy()
    {
        targetTile = Map.StartTile;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
    }
}
