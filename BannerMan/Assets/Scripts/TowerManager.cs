using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]
    public float range = 5f;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "targetObject";

    private GameObject nearestEnemy;
    public GameObject projectilePrefab;
    public Transform projectileSpawn;

    public AudioSource AC_projectileLaunch;
    public AudioSource AC_projectileHit;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        nearestEnemy = null;
            foreach(GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if(distanceToEnemy < shortestDistance)
                {
                    if(enemy != null)
                    {
                        if (enemy.GetComponent<PlayerColorManager>() != null)
                        {
                            if (enemy.GetComponent<PlayerColorManager>().playerID != GetComponent<PlayerColorManager>().playerID)
                            {
                                nearestEnemy = enemy;
                                shortestDistance = distanceToEnemy;
                            }
                        }
                    }
                }
            }

            if(nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
            }

    }
    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }
        if(fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject projectileSpawnedPrefab = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation) as GameObject;
        AC_projectileLaunch.Play();
        ProjectileController projectile = projectileSpawnedPrefab.GetComponent<ProjectileController>();
        projectile.parentTower = GetComponent<TowerManager>();
        projectileSpawnedPrefab.GetComponent<PlayerColorManager>().playerID = GetComponent<PlayerColorManager>().playerID;
        if(projectile != null)
        {
            projectile.Seek(target, nearestEnemy);
        }
    }
    public void ProjectileHit()
    {
        AC_projectileHit.Play();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);    
    }
}
