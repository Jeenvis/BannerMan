using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawnManager : MonoBehaviour
{

    public float growthTime;
    public int resourceAmount;
    public string resourceType;
    bool resourceActive = false;
    public GameObject myResource;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ResourceGrowth());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ResourceGrowth()
    {
        yield return new WaitForSeconds(growthTime);
        if (resourceActive == false)
        {
            myResource.GetComponent<Animator>().SetBool("ResourceActive", true);
            resourceActive = true;
        }
    }
    public void ResourceReset()
    {
        myResource.GetComponent<Animator>().SetBool("ResourceActive", false);
        resourceActive = false;
        StartCoroutine(ResourceGrowth());
    }
}
