using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int healthTotal = 1;
    public int healthCurrent;
    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = healthTotal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageTaken)
    {
        healthCurrent = healthCurrent - damageTaken;
        if (healthCurrent <= 0)
        {
            Destroy(gameObject);
        }
    }
}
