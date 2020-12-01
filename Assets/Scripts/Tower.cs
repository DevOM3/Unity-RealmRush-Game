using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem projectileParticle;

    public WayPoint baseWayPoint;

    // Creating the State targetEnemy
    Transform targetEnemy;

    // Update is called once per frame
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
        
    }

    private void SetTargetEnemy()
    {
        EnemyDamage[] sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach(EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform closestEnemy, Transform testEnemy)
    {
        var distanceToClosestEnemy = Vector3.Distance(transform.position, closestEnemy.position);
        var distanceToTestEnemy = Vector3.Distance(transform.position, testEnemy.position);
        if(distanceToClosestEnemy < distanceToTestEnemy)
        {
            return closestEnemy;
        }
        return testEnemy;
    }

    private void FireAtEnemy()
    {
        float distance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);

        if (distance <= attackRange)
        {
            objectToPan.LookAt(targetEnemy);
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool toShoot)
    {
        ParticleSystem.EmissionModule emission = projectileParticle.emission;
        emission.enabled = toShoot;
    }
}
