using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Transform target;

    [Header("Unity Setup Fields")]
    public string enemyTag = "targetObject";
    public string resourceTag = "grabAbleResource";

    private GameObject nearestEnemy;
    public Vector3 walkTarget;
    public bool doneWalking = false;
    public float sight = 5f;
    public float range = 1f;
    float sightMultiplier = 2;
    public float speed = 1f;
    public int attack = 2;
    private float closeToWalkTarget = 0.2f;
    public GameObject impactEffect;
    public GameObject projectilePrefab;
    public ResourceManager resourceManager;
    public AudioSource AC_projectileHit;
    public AudioSource AC_projectileLaunch;
    public AudioSource AC_GrabResource;
    public float attackSpeed;
    public string unitType;

    // Start is called before the first frame update

    // Update is called once per frame

    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        float walkDist = Vector3.Distance(transform.position, walkTarget);
        if (walkDist > closeToWalkTarget && doneWalking == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkTarget, step);
        }
        else
        {
            doneWalking = true;
        }
        if(doneWalking == true)
        {
            if (target != null)
            {
                float dist = Vector3.Distance(transform.position, target.position);
                if (dist >= range)
                {
                    //implement navmesh navigation
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                }
            }
        }
    }
    void Start()
    {
        transform.LookAt(walkTarget);
        if (unitType == "Warrior")
        {
            InvokeRepeating("UpdateTargetWarrior", 0f, 0.5f);
            InvokeRepeating("MeleeAttack", 0f, attackSpeed);
        }
        if (unitType == "Hunter")
        {
            InvokeRepeating("UpdateTargetHunter", 0f, 0.5f);
            InvokeRepeating("RangedAttack", 0f, attackSpeed);
        }
        if (unitType == "Siege")
        {
            InvokeRepeating("UpdateTargetSiege", 0f, 0.5f);
            InvokeRepeating("MeleeAttack", 0f, attackSpeed);
        }
        if(unitType == "Civilian")
        {
            InvokeRepeating("UpdateTargetCivilian", 0f, 0.5f);
            InvokeRepeating("GatherResource", 0f, attackSpeed);
        }
    }
    void UpdateTargetWarrior()
    {
        if (doneWalking == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    if (enemy != null)
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

            if (nearestEnemy != null && shortestDistance <= (sight * sightMultiplier))
            {
                target = nearestEnemy.transform;
                transform.LookAt(target);
            }
        }
    }
    void UpdateTargetHunter()
    {
        if (doneWalking == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance && enemy.GetComponent<UnitController>() != null)
                {
                    if (enemy != null)
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

            if (nearestEnemy != null && shortestDistance <= (sight * sightMultiplier))
            {
                target = nearestEnemy.transform;
                transform.LookAt(target);
            }
            else if(nearestEnemy == null)
            {
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        if (enemy != null)
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
            }
            if (nearestEnemy != null && shortestDistance <= (sight * sightMultiplier))
            {
                target = nearestEnemy.transform;
                transform.LookAt(target);
            }
        }
    }
    void UpdateTargetSiege()
    {
        if (doneWalking == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance && (enemy.GetComponent<TowerManager>() != null || enemy.GetComponent<ResourceSpawnManager>() != null || enemy.GetComponent<DummyManager>() != null))
                {
                    if (enemy != null)
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

            if (nearestEnemy != null && shortestDistance <= (sight * sightMultiplier))
            {
                target = nearestEnemy.transform;
                transform.LookAt(target);
            }
            else if (nearestEnemy == null)
            {
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        if (enemy != null)
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
            }
            if (nearestEnemy != null && shortestDistance <= (sight * sightMultiplier))
            {
                target = nearestEnemy.transform;
                transform.LookAt(target);
            }
        }
    }
    void UpdateTargetCivilian()
    {
        if (doneWalking == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance && enemy.GetComponent<ResourceSpawnManager>() != null)
                {
                    if (enemy != null)
                    {
                        if (enemy.GetComponent<PlayerColorManager>() != null)
                        {
                            if (enemy.GetComponent<PlayerColorManager>().playerID == GetComponent<PlayerColorManager>().playerID && enemy.GetComponent<ResourceSpawnManager>().resourceActive == true)
                            {
                                nearestEnemy = enemy;
                                shortestDistance = distanceToEnemy;
                            }
                        }
                    }
                }
            }

            if (nearestEnemy != null && shortestDistance <= (sight * sightMultiplier))
            {
                target = nearestEnemy.transform;
                transform.LookAt(target);
            }
            else if (doneWalking == true)
            {
                enemies = GameObject.FindGameObjectsWithTag(resourceTag);
                shortestDistance = Mathf.Infinity;
                nearestEnemy = null;
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < shortestDistance && enemy.GetComponent<GrabAbleResourceManager>() != null)
                    {
                        if (enemy != null)
                        {
                            nearestEnemy = enemy;
                            shortestDistance = distanceToEnemy;
                        }
                    }
                }

                if (nearestEnemy != null && shortestDistance <= (sight * sightMultiplier))
                {
                    target = nearestEnemy.transform;
                    transform.LookAt(target);
                }
            }
        
        }
    }
    void MeleeAttack()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist <= range)
            {
                if (nearestEnemy.GetComponent<HealthManager>() != null)
                {
                    nearestEnemy.GetComponent<HealthManager>().TakeDamage(attack);
                    GameObject collisionDust = (GameObject)Instantiate(impactEffect, target.transform.position + new Vector3(0, 1, 0), transform.rotation);
                    Destroy(collisionDust, 2f);
                }
            }
        }
    }
    void RangedAttack()
    {
        GameObject projectileSpawnedPrefab = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
        AC_projectileLaunch.Play();
        ProjectileController projectile = projectileSpawnedPrefab.GetComponent<ProjectileController>();
        projectile.parentUnit = GetComponent<UnitController>();
        projectileSpawnedPrefab.GetComponent<PlayerColorManager>().playerID = GetComponent<PlayerColorManager>().playerID;
            if (projectile != null)
        {
            projectile.Seek(target, nearestEnemy);
        }
    }
    void GatherResource()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist <= range)
            {
                if (nearestEnemy.GetComponent<GrabAbleResourceManager>() != null)
                {
                    if (nearestEnemy.GetComponent<GrabAbleResourceManager>().resourceType == "wood" && nearestEnemy.GetComponent<GrabAbleResourceManager>().grabbed == false)
                    {
                        resourceManager.wood = resourceManager.wood + nearestEnemy.GetComponent<GrabAbleResourceManager>().resourceAmount;
                        nearestEnemy.GetComponent<GrabAbleResourceManager>().grabbed = true;
                        resourceManager.ChangeUI();
                        nearestEnemy.GetComponent<Animator>().SetTrigger("Grabbed");
                        AC_GrabResource.Play();
                    }
                    Destroy(nearestEnemy, 1f);
                }
                if (nearestEnemy.GetComponent<ResourceSpawnManager>() != null)
                {
                    if (nearestEnemy.GetComponent<ResourceSpawnManager>().resourceType == "wood")
                    {
                        resourceManager.wood = resourceManager.wood + nearestEnemy.GetComponent<ResourceSpawnManager>().resourceAmount;
                    }
                    if (nearestEnemy.GetComponent<ResourceSpawnManager>().resourceType == "food")
                    {
                        resourceManager.food = resourceManager.food + nearestEnemy.GetComponent<ResourceSpawnManager>().resourceAmount;
                    }
                    AC_GrabResource.Play();
                    resourceManager.ChangeUI();
                    nearestEnemy.GetComponent<ResourceSpawnManager>().ResourceReset();
                }
            }
        }
    }
    void SearchForTarget(string searchTag, string searchFor)//Create a basic function for searching for targets
    {
        if (doneWalking == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(searchTag);
            float shortestDistance = Mathf.Infinity;
            nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                //^Repeatable part
                if ((searchFor == "bariccades" && distanceToEnemy < shortestDistance)
                    || (searchFor == "buildings" && distanceToEnemy < shortestDistance && (enemy.GetComponent<TowerManager>() != null || enemy.GetComponent<ResourceSpawnManager>() != null || enemy.GetComponent<DummyManager>() != null))
                    || (searchFor == "units" && distanceToEnemy < shortestDistance && enemy.GetComponent<UnitController>() != null)
                    || (searchFor == "unitsAndBuildings" && distanceToEnemy < shortestDistance)
                    || (searchFor == "grabAbleResources" && distanceToEnemy < shortestDistance && enemy.GetComponent<GrabAbleResourceManager>() != null)
                    || (searchFor == "spawnAbleResources" && distanceToEnemy < shortestDistance && enemy.GetComponent<ResourceSpawnManager>() != null && enemy.GetComponent<ResourceSpawnManager>().resourceActive == true){ 
                    if (enemy != null)//repeatable
                    {
                        if (enemy.GetComponent<PlayerColorManager>() != null)//repeatable
                        {
                            if (enemy.GetComponent<PlayerColorManager>().playerID != GetComponent<PlayerColorManager>().playerID)//repeatable execpt of resources / civilian
                            {
                                nearestEnemy = enemy; // repeatable
                                shortestDistance = distanceToEnemy; // repeatable
                            }
                        }
                    }
                }
            }

            if (nearestEnemy != null && shortestDistance <= (sight * sightMultiplier)) // repeatable
            {
                target = nearestEnemy.transform; // repeatable
                transform.LookAt(target); // repeatable
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
    public void ProjectileHit()
    {
        AC_projectileHit.Play();
    }
}
