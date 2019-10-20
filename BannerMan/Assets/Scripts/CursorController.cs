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
    public AudioSource AC_GrabResource;
    public GameObject buildMenu;
    public GameObject trainMenu;
    public SceneManager sceneManager;
    public GameObject[] buildMenuOptions;
    public GameObject[] trainMenuOptions;
    public Animator buildRef;
    public Animator trainRef;
    int buildMenuOptionSelection;
    int trainMenuOptionSelection;
    public GameObject resourceTarget;
    public Transform unitSpawn;

    bool buildTriggerActivation = false;
    bool trainTriggerActivation = false;
    public GameObject towerObj;
    public GameObject bariccadeObj;
    public GameObject civilianObj;
    public GameObject warriorObj;
    public GameObject hunterObj;
    public GameObject siegeObj;
    public GameObject farmObj;
    public GameObject plantationObj;
    public ResourceManager resourceManager;

    int towerCost = 2;
    int farmCost = 2;
    int warriorCost = 2;
    int civilianCost = 2;
    int hunterCost = 2;
    int siegeCost = 2;
    int bariccadeCost = 2;
    int plantationCost = 2;
    public int collidersWithRef;

    public bool inUnitBoundingBox;
    public bool inBuildingBoundingBox;
    public Material circleCursor;
    public Material squareCursor;
    public Material crossCursor;

    void Start()
    {
        walkSpeed = 10;
    }

    void FixedUpdate()
    {
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;
        // Move senteces
        rb.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f), 0, Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));
        if (Input.GetAxisRaw("Joy1_3thAxis") == 0)
        {
            if (buildTriggerActivation == true)
            {
                if (buildMenuOptionSelection != -1)
                {

                    switch (buildMenuOptionSelection)
                    {
                        case 0:
                            if (resourceManager.wood >= farmCost && collidersWithRef <= 0 && inBuildingBoundingBox == true)
                            {
                                AC_BuildObject.Play();
                                GameObject spawnFarmObj = Instantiate(farmObj, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnFarmObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                spawnFarmObj.GetComponent<PlayerColorManager>().SetColor();
                                resourceManager.wood = resourceManager.wood - farmCost;
                                resourceManager.ChangeUI();
                            }
                            else
                            {
                                AC_CancelBuild.Play();
                            }
                            break;
                        case 1:
                            if (resourceManager.wood >= plantationCost && collidersWithRef <= 0 && inBuildingBoundingBox == true)
                            {
                                AC_BuildObject.Play();
                                GameObject spawnPlantationObj = Instantiate(plantationObj, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnPlantationObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                spawnPlantationObj.GetComponent<PlayerColorManager>().SetColor();
                                resourceManager.wood = resourceManager.wood - plantationCost;
                                resourceManager.ChangeUI();
                            }
                            else
                            {
                                AC_CancelBuild.Play();
                            }
                            break;
                        case 2:
                            if (resourceManager.wood >= bariccadeCost && collidersWithRef <= 0 && inBuildingBoundingBox == true)
                            {
                                AC_BuildObject.Play();
                                GameObject spawnBarricadeObj = Instantiate(bariccadeObj, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnBarricadeObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                spawnBarricadeObj.GetComponent<PlayerColorManager>().SetColor();
                                resourceManager.wood = resourceManager.wood - bariccadeCost;
                                resourceManager.ChangeUI();
                            }
                            break;
                        case 3:
                            if (resourceManager.wood >= towerCost && collidersWithRef <= 0 && inBuildingBoundingBox == true)
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
                if (trainTriggerActivation == true)
                {
                    if (trainMenuOptionSelection != -1)
                    {

                        switch (trainMenuOptionSelection)
                        {
                            case 0:
                            if (resourceManager.food >= civilianCost && inUnitBoundingBox == true)
                            {
                                AC_TrainObject.Play();
                                GameObject spawnCivilianObj = Instantiate(civilianObj, unitSpawn.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnCivilianObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                spawnCivilianObj.GetComponent<PlayerColorManager>().SetColor();
                                spawnCivilianObj.GetComponent<UnitController>().walkTarget = transform.position;
                                spawnCivilianObj.GetComponent<UnitController>().resourceManager = resourceManager;
                                resourceManager.food = resourceManager.food - civilianCost;
                                resourceManager.ChangeUI();
                            }
                            else
                            {
                                AC_CancelBuild.Play();
                            }
                            break;
                            case 1:
                                if (resourceManager.food >= warriorCost && inUnitBoundingBox == true)
                                {
                                    AC_TrainObject.Play();
                                    GameObject spawnWarriorObj = Instantiate(warriorObj, unitSpawn.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                    spawnWarriorObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                    spawnWarriorObj.GetComponent<PlayerColorManager>().SetColor();
                                    spawnWarriorObj.GetComponent<UnitController>().walkTarget = transform.position;
                                    resourceManager.food = resourceManager.food - warriorCost;
                                    resourceManager.ChangeUI();
                                }
                                else
                                {
                                    AC_CancelBuild.Play();
                                }
                                break;
                            case 2:
                            if (resourceManager.food >= hunterCost && inUnitBoundingBox == true)
                            {
                                AC_TrainObject.Play();
                                GameObject spawnHunterObj = Instantiate(hunterObj, unitSpawn.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnHunterObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                spawnHunterObj.GetComponent<PlayerColorManager>().SetColor();
                                spawnHunterObj.GetComponent<UnitController>().walkTarget = transform.position;
                                resourceManager.food = resourceManager.food - hunterCost;
                                resourceManager.ChangeUI();
                            }
                            else
                            {
                                AC_CancelBuild.Play();
                            }
                            break;
                            case 3:
                            if (resourceManager.food >= siegeCost && inUnitBoundingBox == true)
                            {
                                AC_TrainObject.Play();
                                GameObject spawnSiegeObj = Instantiate(siegeObj, unitSpawn.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnSiegeObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                spawnSiegeObj.GetComponent<PlayerColorManager>().SetColor();
                                spawnSiegeObj.GetComponent<UnitController>().walkTarget = transform.position;
                                resourceManager.food = resourceManager.food - siegeCost;
                                resourceManager.ChangeUI();
                            }
                            else
                            {
                                AC_CancelBuild.Play();
                            }
                            break;
                        }
                        
                    }
                trainMenu.GetComponent<Animator>().SetTrigger("PopDown");
                trainTriggerActivation = false;
                
                }
            collidersWithRef = 0;
        }
        if (Input.GetAxisRaw("Joy1_3thAxis") < 0 && buildTriggerActivation == false)
        {
            BuildTrigger();
            buildTriggerActivation = true;
        }
        if (Input.GetAxisRaw("Joy1_3thAxis") > 0 && trainTriggerActivation == false)
        {
            TrainTrigger();
            trainTriggerActivation = true;
        }
        if (buildTriggerActivation == true)
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
            buildRef.SetInteger("buildMenuOption", buildMenuOptionSelection);
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
        if (trainTriggerActivation == true)
        {
            if (Input.GetAxisRaw("Joy1_secondHorizontal") < -0.2f)
            {
                trainMenuOptionSelection = 3;
            }
            if (Input.GetAxisRaw("Joy1_secondHorizontal") > 0.2f)
            {
                trainMenuOptionSelection = 1;
            }
            if (Input.GetAxisRaw("Joy1_secondVertical") < -0.2f)
            {
                trainMenuOptionSelection = 0;
            }
            if (Input.GetAxisRaw("Joy1_secondVertical") > 0.2f)
            {
                trainMenuOptionSelection = 2;
            }
            if (Input.GetAxisRaw("Joy1_secondVertical") == 0 && Input.GetAxisRaw("Joy1_secondHorizontal") == 0)
            {
                trainMenuOptionSelection = -1;
            }
            trainRef.SetInteger("trainMenuOption", trainMenuOptionSelection);
            for (int i = 0; i < trainMenuOptions.Length; i++)
            {
                if (i != trainMenuOptionSelection)
                {
                    trainMenuOptions[i].GetComponent<Renderer>().material.SetColor("_Color", trainMenuOptions[i].GetComponent<PlayerColorManager>().myColor);
                }
                else
                {
                    trainMenuOptions[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                }
            }
        }
        if (Input.GetButtonDown("Joy1_AButton"))
        {
            if(resourceTarget != null)
            {
                GatherResource();
            }
        }
    }
    void BuildTrigger()
    {
        AC_BuildMenu.Play();
        buildMenu.GetComponent<Animator>().SetTrigger("PopUp");
    }
    void TrainTrigger()
    {
        AC_BuildMenu.Play();
        trainMenu.GetComponent<Animator>().SetTrigger("PopUp");
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "grabAbleResource")
        {
            resourceTarget = col.gameObject;
        }
        if(col.gameObject.tag == "targetObject" && col.GetComponent<ResourceSpawnManager>() != null)
        {
            resourceTarget = col.gameObject;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if(resourceTarget == col.gameObject)
        {
            resourceTarget = null;
        }
    }
    void GatherResource()
    {
        if(resourceTarget.GetComponent<GrabAbleResourceManager>() != null )
        {
            if(resourceTarget.GetComponent<GrabAbleResourceManager>().resourceType == "wood" && resourceTarget.GetComponent<GrabAbleResourceManager>().grabbed == false)
            {
                resourceManager.wood = resourceManager.wood + resourceTarget.GetComponent<GrabAbleResourceManager>().resourceAmount;
                resourceTarget.GetComponent<GrabAbleResourceManager>().grabbed = true;
                resourceManager.ChangeUI();
                resourceTarget.GetComponent<Animator>().SetTrigger("Grabbed");
                AC_GrabResource.Play();
            }
            Destroy(resourceTarget, 1f);
        }
        if(resourceTarget.GetComponent<ResourceSpawnManager>() != null)
        {
            if(resourceTarget.GetComponent<ResourceSpawnManager>().resourceType == "wood")
            {
                resourceManager.wood = resourceManager.wood + resourceTarget.GetComponent<ResourceSpawnManager>().resourceAmount;
            }
            if (resourceTarget.GetComponent<ResourceSpawnManager>().resourceType == "food")
            {
                resourceManager.food = resourceManager.food + resourceTarget.GetComponent<ResourceSpawnManager>().resourceAmount;
            }
            AC_GrabResource.Play();
            resourceManager.ChangeUI();
            resourceTarget.GetComponent<ResourceSpawnManager>().ResourceReset();
        }
    }
}