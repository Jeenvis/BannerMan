using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject selectedUnit;
    public GameObject hooveredUnit;
    public Text hooverUnitName;
    public Image hooverUnitAvatar;
    public Text hooverUnitAttack;
    public Text hooverUnitAttackSpeed;
    public Text hooverUnitHealth;
    public Text hooverUnitRange;
    public Text hooverUnitMovementSpeed;

    void Start()
    {
        walkSpeed = 10;
        GetComponent<LineRenderer>().SetColors(GetComponent<PlayerColorManager>().myColor, GetComponent<PlayerColorManager>().myColor);
    }

    void FixedUpdate()
    {
        if (selectedUnit != null && GetComponent<LineRenderer>() != null)
        {
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, selectedUnit.transform.position);
        }
        curSpeed = walkSpeed;
        maxSpeed = curSpeed;
        // Move senteces
        if (playerID == 1) {
            rb.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal1") * curSpeed, 0.8f), 0, Mathf.Lerp(0, Input.GetAxis("Vertical1") * curSpeed, 0.8f));
        }
        if(playerID == 2) { 
            rb.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal2") * curSpeed, 0.8f), 0, Mathf.Lerp(0, Input.GetAxis("Vertical2") * curSpeed, 0.8f));
        }
        if ((Input.GetAxisRaw("Joy1_3thAxis") == 0 && playerID == 1) || (Input.GetAxisRaw("Joy2_3thAxis") == 0 && playerID == 2))
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
        if ((Input.GetAxisRaw("Joy1_3thAxis") < 0 && buildTriggerActivation == false && playerID == 1) || (Input.GetAxisRaw("Joy2_3thAxis") < 0 && buildTriggerActivation == false && playerID == 2))
        {
            BuildTrigger();
            buildTriggerActivation = true;
        }
        if ((Input.GetAxisRaw("Joy1_3thAxis") > 0 && trainTriggerActivation == false && playerID == 1) || (Input.GetAxisRaw("Joy2_3thAxis") > 0 && trainTriggerActivation == false && playerID == 2))
        {
            TrainTrigger();
            trainTriggerActivation = true;
        }
        if (buildTriggerActivation == true)
        {
            if ((Input.GetAxisRaw("Joy1_secondHorizontal") < -0f && playerID == 1) || (Input.GetAxisRaw("Joy2_secondHorizontal") < -0f && playerID == 2))
            {
                buildMenuOptionSelection = 3;
            }
            if ((Input.GetAxisRaw("Joy1_secondHorizontal") > 0f && playerID == 1) || (Input.GetAxisRaw("Joy2_secondHorizontal") > 0f && playerID == 2))
            {
                buildMenuOptionSelection = 1;
            }
            if ((Input.GetAxisRaw("Joy1_secondVertical") < -0f && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") < -0f && playerID == 2))
            {
                buildMenuOptionSelection = 0;
            }
            if ((Input.GetAxisRaw("Joy1_secondVertical") > 0f && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") > 0f && playerID == 2))
            {
                buildMenuOptionSelection = 2;
            }
            if((Input.GetAxisRaw("Joy1_secondVertical")  ==0 && Input.GetAxisRaw("Joy1_secondHorizontal")  ==0 && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") ==0 && Input.GetAxisRaw("Joy2_secondHorizontal")  ==0 && playerID == 2))
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
            if ((Input.GetAxisRaw("Joy1_secondHorizontal") < -0f && playerID == 1) || (Input.GetAxisRaw("Joy2_secondHorizontal") < -0f && playerID == 2))
            {
                trainMenuOptionSelection = 3;
            }
            if ((Input.GetAxisRaw("Joy1_secondHorizontal") > 0f && playerID == 1) || (Input.GetAxisRaw("Joy2_secondHorizontal") > 0f && playerID == 2))
            {
                trainMenuOptionSelection = 1;
            }
            if ((Input.GetAxisRaw("Joy1_secondVertical") < -0f && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") < -0f && playerID == 2))
            {
                trainMenuOptionSelection = 0;
            }
            if ((Input.GetAxisRaw("Joy1_secondVertical") > 0f && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") > 0f && playerID == 2))
            {
                trainMenuOptionSelection = 2;
            }
            if ((Input.GetAxisRaw("Joy1_secondVertical") ==0 && Input.GetAxisRaw("Joy1_secondHorizontal")  ==0 && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") ==0 && Input.GetAxisRaw("Joy2_secondHorizontal") ==0 && playerID == 2))
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
        if ((Input.GetButtonDown("Joy1_AButton") && playerID == 1) || (Input.GetButtonDown("Joy2_AButton") && playerID == 2))
        {
            if(resourceTarget != null)
            {
                GatherResource();
            }
            if (selectedUnit != null)
            {
                selectedUnit.GetComponent<UnitController>().walkTarget = transform.position;
                selectedUnit.GetComponent<UnitController>().doneWalking = false;
                GetComponent<LineRenderer>().enabled = false;
                hooveredUnit = null;
                selectedUnit = null;
            }
            if (hooveredUnit != null && hooveredUnit.GetComponent<UnitController>() != null && selectedUnit == null)
            {
                selectedUnit = hooveredUnit;
                GetComponent<LineRenderer>().enabled = true;
            }
            
        }
        if ((Input.GetButtonDown("Joy1_BButton") && playerID == 1) || (Input.GetButtonDown("Joy2_BButton") && playerID == 2))
        {
            if (selectedUnit != null)
            {
                selectedUnit = null;
                GetComponent<LineRenderer>().enabled = false;
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
        if (col.gameObject.GetComponent<HooverData>() != null && selectedUnit == null)
        {
            hooverUnitName.text = col.gameObject.GetComponent<HooverData>().name.ToString();
            hooverUnitAvatar.sprite = col.gameObject.GetComponent<HooverData>().avatar;
            hooverUnitAttack.text = col.gameObject.GetComponent<HooverData>().attack.ToString();
            hooverUnitAttackSpeed.text = col.gameObject.GetComponent<HooverData>().attackSpeed.ToString();
            hooverUnitMovementSpeed.text = col.gameObject.GetComponent<HooverData>().movementSpeed.ToString();
            hooverUnitRange.text = col.gameObject.GetComponent<HooverData>().range.ToString();
            hooverUnitHealth.text = col.gameObject.GetComponent<HooverData>().health.ToString();
            hooveredUnit = col.gameObject;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if(resourceTarget == col.gameObject)
        {
            resourceTarget = null;
        }
        if (col.gameObject.GetComponent<HooverData>() != null && selectedUnit == null)
        {
            hooverUnitName.text = "";
            hooverUnitAttack.text = "";
            hooverUnitAttackSpeed.text = "";
            hooverUnitMovementSpeed.text = "";
            hooverUnitRange.text = "";
            hooverUnitHealth.text = "";
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