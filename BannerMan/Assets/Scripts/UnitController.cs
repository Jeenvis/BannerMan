using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Transform target;

    [Header("Unity Setup Fields")]
    public string enemyTag = "targetObject";

    private GameObject nearestEnemy;
    public Vector3 walkTarget;
    public bool doneWalking = false;
    public float sight = 5f;
    public float range = 1f;
    float sightMultiplier = 2;
    public float speed = 1f;
    private float closeToWalkTarget = 0.2f;
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
                if (dist > range)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                }
            }
        }
    }
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
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
            }
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
