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
        string[] searchForTargetTags = new string[] { enemyTag, enemyTag };
        string[] searchForTargetTasks = new string[] { "bariccades", "unitsAndBuildings" };
        SearchForTarget(searchForTargetTags, searchForTargetTasks);
    }
    void SearchForTarget(string[] searchTag, string[] searchFor)
    {
            for (int i = 0; i < searchFor.Length; i++)
            {
                if (target != null)
                {
                    return;
                }
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(searchTag[i]);
                float shortestDistance = Mathf.Infinity;
                nearestEnemy = null;
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    //^Repeatable part
                    if ((searchFor[i] == "bariccades" && distanceToEnemy < shortestDistance && distanceToEnemy < range && enemy.GetComponent<BariccadeManager>() != null)
                        || (searchFor[i] == "buildings" && distanceToEnemy < shortestDistance && (enemy.GetComponent<TowerManager>() != null || enemy.GetComponent<ResourceSpawnManager>() != null || enemy.GetComponent<DummyManager>() != null || enemy.GetComponent<CastleManager>() != null) && distanceToEnemy < range)
                        || (searchFor[i] == "units" && distanceToEnemy < shortestDistance && enemy.GetComponent<UnitController>() != null && distanceToEnemy < range)
                        || (searchFor[i] == "unitsAndBuildings" && distanceToEnemy < shortestDistance && distanceToEnemy < range)
                        || (searchFor[i] == "grabAbleResources" && distanceToEnemy < shortestDistance && enemy.GetComponent<GrabAbleResourceManager>() != null && distanceToEnemy < range)
                        || (searchFor[i] == "spawnAbleResources" && distanceToEnemy < shortestDistance && enemy.GetComponent<ResourceSpawnManager>() != null && enemy.GetComponent<ResourceSpawnManager>().resourceActive == true && distanceToEnemy < range)
                        || (searchFor[i] == "anyTargetAnyRange" && distanceToEnemy < shortestDistance))
                    {
                        if (enemy != null)//repeatable
                        {
                            if ((searchFor[i] != "grabAbleResources" && enemy.GetComponent<PlayerColorManager>() != null) || searchFor[i] == "grabAbleResources" && enemy.GetComponent<GrabAbleResourceManager>() != null)// only attacking repeatable
                            {
                                if ((searchFor[i] == "grabAbleResources")
                                    || (searchFor[i] != "spawnAbleResources" && enemy.GetComponent<PlayerColorManager>().playerID != GetComponent<PlayerColorManager>().playerID)
                                    || (searchFor[i] == "spawnAbleResources" && enemy.GetComponent<PlayerColorManager>().playerID == GetComponent<PlayerColorManager>().playerID)
                                    )//repeatable execpt of resources / civilian
                                {
                                    nearestEnemy = enemy; // repeatable
                                    shortestDistance = distanceToEnemy; // repeatable
                                }
                            }
                        }
                    }
                }
                if (nearestEnemy != null && shortestDistance <= (range)) // repeatable
                {
                    target = nearestEnemy.transform; // repeatable
                }
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
