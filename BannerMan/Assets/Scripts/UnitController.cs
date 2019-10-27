using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Transform target;

    [Header("Unity Setup Fields")]
    public string enemyTag = "targetObject";
    public string resourceTag = "grabAbleResource";

    public GameObject nearestEnemy;
    public Vector3 walkTarget;
    public bool doneWalking = false;
    public float sight = 5f;
    public float range = 1f;
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
    public Animator myAnimator;

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
    void UpdateTargetWarrior()          //RETHINK IF I WANT UNITS TO EXPLORE WHEN THERE IS NOTHING IN SIGHT OR TO STAY PUT UNTIL AN ENEMY COMES INTO SIGHT
    {
        string[] searchForTargetTags = new string[] { enemyTag, enemyTag };
        string[] searchForTargetTasks = new string[] { "bariccades", "unitsAndBuildings" };
        SearchForTarget(searchForTargetTags, searchForTargetTasks);
    }
    void UpdateTargetHunter()           //RETHINK IF I WANT UNITS TO EXPLORE WHEN THERE IS NOTHING IN SIGHT OR TO STAY PUT UNTIL AN ENEMY COMES INTO SIGHT
    {
        string[] searchForTargetTags = new string[] { enemyTag, enemyTag, enemyTag };
        string[] searchForTargetTasks = new string[] { "bariccades", "units", "unitsAndBuildings" };
        SearchForTarget(searchForTargetTags, searchForTargetTasks);
    }
    void UpdateTargetSiege()            //RETHINK IF I WANT UNITS TO EXPLORE WHEN THERE IS NOTHING IN SIGHT OR TO STAY PUT UNTIL AN ENEMY COMES INTO SIGHT
    {
        string[] searchForTargetTags = new string[] { enemyTag, enemyTag, enemyTag };
        string[] searchForTargetTasks = new string[] { "bariccades","buildings", "unitsAndBuildings" };
        SearchForTarget(searchForTargetTags, searchForTargetTasks);
    }
    void UpdateTargetCivilian()         
    {
        string[] searchForTargetTags = new string[] { enemyTag, resourceTag};
        string[] searchForTargetTasks = new string[] { "spawnAbleResources", "grabAbleResources" };
        SearchForTarget(searchForTargetTags, searchForTargetTasks);
    }
    void MeleeAttack()
    {
        if (target != null)
        {
            if (nearestEnemy == null)
            {
                nearestEnemy = target.gameObject;
            }
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist <= range && nearestEnemy != null)
            {
                if (nearestEnemy.GetComponent<HealthManager>() != null)
                {
                    if (myAnimator != null)
                    {
                        myAnimator.GetComponent<Animator>().SetTrigger("MeleeAttack");
                    }
                    nearestEnemy.GetComponent<HealthManager>().TakeDamage(attack);
                    GameObject collisionDust = (GameObject)Instantiate(impactEffect, target.transform.position + new Vector3(0, 1, 0), transform.rotation);
                    Destroy(collisionDust, 2f);
                }
            }
        }
    }
    void RangedAttack()
    {
        if (target != null)
        {
            if(nearestEnemy == null)
            {
                nearestEnemy = target.gameObject;
            }
            if (myAnimator != null)
            {
                myAnimator.GetComponent<Animator>().SetTrigger("RangedAttack");
            }
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
    }
    void GatherResource()
    {
        if (target != null) { 
            if(nearestEnemy == null)
            {
                nearestEnemy = target.gameObject;
            }
            
            float dist = Vector3.Distance(transform.position, target.position);
            if (dist <= range && nearestEnemy != null && resourceManager != null)
            {
                if (nearestEnemy.GetComponent<GrabAbleResourceManager>() != null)
                {
                    if (nearestEnemy.GetComponent<GrabAbleResourceManager>().resourceType == "wood" && nearestEnemy.GetComponent<GrabAbleResourceManager>().grabbed == false)
                    {
                        if (myAnimator != null)
                        {
                            myAnimator.GetComponent<Animator>().SetTrigger("MeleeAttack");
                        }
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
                    if (nearestEnemy.GetComponent<ResourceSpawnManager>().resourceActive == true)
                    {
                        if (myAnimator != null)
                        {
                            myAnimator.GetComponent<Animator>().SetTrigger("MeleeAttack");
                        }
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
                        target = null;
                    }
                }
            }
        }
    }
    void SearchForTarget(string[] searchTag, string[] searchFor)
    {
        if (doneWalking == true)
        {
            for (int i = 0; i < searchFor.Length; i++)
            {
                if(target != null && i > 0)
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
                    if ((searchFor[i] == "bariccades" && distanceToEnemy < shortestDistance && distanceToEnemy < sight && enemy.GetComponent<BariccadeManager>()!= null)
                        || (searchFor[i] == "buildings" && distanceToEnemy < shortestDistance && (enemy.GetComponent<TowerManager>() != null || enemy.GetComponent<ResourceSpawnManager>() != null || enemy.GetComponent<DummyManager>() != null || enemy.GetComponent<CastleManager>() != null) && distanceToEnemy < sight)
                        || (searchFor[i] == "units" && distanceToEnemy < shortestDistance && enemy.GetComponent<UnitController>() != null && distanceToEnemy < sight)
                        || (searchFor[i] == "unitsAndBuildings" && distanceToEnemy < shortestDistance && distanceToEnemy < sight)
                        || (searchFor[i] == "grabAbleResources" && distanceToEnemy < shortestDistance && enemy.GetComponent<GrabAbleResourceManager>() != null && distanceToEnemy < sight)
                        || (searchFor[i] == "spawnAbleResources" && distanceToEnemy < shortestDistance && enemy.GetComponent<ResourceSpawnManager>() != null && enemy.GetComponent<ResourceSpawnManager>().resourceActive == true && distanceToEnemy < sight)
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
                if (nearestEnemy != null && shortestDistance <= (sight)) // repeatable
                {
                    target = nearestEnemy.transform; // repeatable
                    transform.LookAt(target); // repeatable
               }
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
