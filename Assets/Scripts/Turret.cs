﻿using UnityEngine;

public class Turret : GameTileContent
{

	[SerializeField, Range(1.5f, 10.5f)]
	float targetingRange = 1.5f;

	[SerializeField, Range(1f, 100f)]
	float damagePerSecond = 10f;

	[SerializeField, Range(1f, 100f)]
	float fireRate = 1f;

    public Transform partToRotate;

    private float turnSpeed = 6.5f;

    private float fireCountdown = 0f;

	public string enemyTag = "Enemy";
	private Transform target;
    public Enemy Enemy { get; private set; }

    public GameObject bulletPrefab;
	public Transform firePoint;

	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in ennemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= targetingRange)
        {
            target = nearestEnemy.transform;
            Enemy = target.GetComponent<Enemy>();
            Enemy.ApplyDamage(damagePerSecond);
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1 / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}