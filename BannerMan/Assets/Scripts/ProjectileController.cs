using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private Transform target;
    private GameObject gOTarget;
    public TowerManager parentTower;
    public UnitController parentUnit;
    public GameObject impactEffect;
    public float speed;
    public int damage;

    // Start is called before the first frame update

    public void Seek(Transform _target, GameObject _gOTarget)
    {
        target = _target;
        gOTarget = _gOTarget;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.LookAt(target);
        Vector3 dir = (target.position + new Vector3(0,1,0))- transform.position;
        float distanceThisFrame = speed * (Time.deltaTime);

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {
        GameObject collisionDust = (GameObject)Instantiate(impactEffect, transform.position + new Vector3(0,1,0), transform.rotation);
        Destroy(collisionDust, 2f);
        if (gOTarget.GetComponent<HealthManager>() != null) {
            gOTarget.GetComponent<HealthManager>().TakeDamage(damage);
        }
        if (parentTower != null)
        {
            parentTower.ProjectileHit();
        }
        else if(parentUnit != null)
        {
            parentUnit.ProjectileHit();
        }
        Destroy(gameObject);
    }
    

}
