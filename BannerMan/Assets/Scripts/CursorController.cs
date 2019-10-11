using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    // Normal Movements Variables
    public int playerID;
    private float walkSpeed;
    private float curSpeed;
    private float maxSpeed;
    public Rigidbody rb;

    public AudioSource AC_BuildMenu;
    public AudioSource AC_BuildObject;
    public AudioSource AC_TrainObject;
    public AudioSource AC_CancelBuild;
    public GameObject buildMenu;
    public GameObject[] buildMenuOptions;
    int buildMenuOptionSelection;

    bool buildTriggerActivation = false;
    public GameObject towerObj;
    public ResourceManager resourceManager;

    int towerCost = 2;
    int warriorCost = 2;

    void Start()
    {
        walkSpeed = 5;
    }

    void FixedUpdate()
    {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;
        // Move senteces
        rb.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f), 0, Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
        if (Input.GetAxisRaw("Joy1_3thAxis") == 0)
        {
            if(buildTriggerActivation == true)
            {
                if (buildMenuOptionSelection != -1)
                {
                    
                    switch (buildMenuOptionSelection)
                    {
                        case 0:
                            AC_BuildObject.Play();
                            break;
                        case 1:
                            if (resourceManager.food >= warriorCost)
                            {
                                AC_TrainObject.Play();
                                resourceManager.food = resourceManager.food - warriorCost;
                                resourceManager.ChangeUI();
                            }
                            else
                            {
                                AC_CancelBuild.Play();
                            }
                            break;
                        case 2:
                            AC_BuildObject.Play();
                            break;
                        case 3:
                            if (resourceManager.wood >= towerCost)
                            {
                                AC_BuildObject.Play();
                                GameObject spawnTowerObj = Instantiate(towerObj, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnTowerObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                spawnTowerObj.GetComponent<PlayerColorManager>().SetColor();
                                resourceManager.wood = resourceManager.wood - towerCost;
                                resourceManager.ChangeUI();
                            }
                            else
                            {
                                AC_CancelBuild.Play();
                            }
                            break;
                    }
                }
                buildMenu.GetComponent<Animator>().SetTrigger("PopDown");
                buildTriggerActivation = false;
            }
        }
        if (Input.GetAxisRaw("Joy1_3thAxis") != 0 && buildTriggerActivation == false)
        {
            BuildTrigger();
            buildTriggerActivation = true;
        }
        if(buildTriggerActivation == true)
        {
            if (Input.GetAxisRaw("Joy1_secondHorizontal") < -0.2f)
            {
                buildMenuOptionSelection = 3;
            }
            if (Input.GetAxisRaw("Joy1_secondHorizontal") > 0.2f)
            {
                buildMenuOptionSelection = 1;
            }
            if (Input.GetAxisRaw("Joy1_secondVertical") < -0.2f)
            {
                buildMenuOptionSelection = 0;
            }
            if (Input.GetAxisRaw("Joy1_secondVertical") > 0.2f)
            {
                buildMenuOptionSelection = 2;
            }
            if(Input.GetAxisRaw("Joy1_secondVertical") == 0 && Input.GetAxisRaw("Joy1_secondHorizontal") == 0)
            {
                buildMenuOptionSelection = -1;
            }
            for (int i = 0; i < buildMenuOptions.Length; i++)
            {
                if (i != buildMenuOptionSelection)
                {
                    buildMenuOptions[i].GetComponent<Renderer>().material.SetColor("_Color", buildMenuOptions[i].GetComponent<PlayerColorManager>().myColor);
                }
                else
                {
                    buildMenuOptions[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                }
            }
        }
    }
    void BuildTrigger()
    {
        AC_BuildMenu.Play();
        buildMenu.GetComponent<Animator>().SetTrigger("PopUp");
    }
}