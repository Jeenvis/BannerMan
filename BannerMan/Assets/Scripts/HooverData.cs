using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HooverData : MonoBehaviour
{
    public string name;
    public int attack;
    public float attackSpeed;
    public float movementSpeed;
    public float range;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        name = gameObject.name;
        if (GetComponent<UnitController>() != null)
        {
            attack = GetComponent<UnitController>().attack;
            attackSpeed = GetComponent<UnitController>().attackSpeed;
            movementSpeed = GetComponent<UnitController>().speed;
            range = GetComponent<UnitController>().range;
        }
        if (GetComponent<HealthManager>() != null)
        {
            health = GetComponent<HealthManager>().healthCurrent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
