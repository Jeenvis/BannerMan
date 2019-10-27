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

    float sensitivity = 0.1f;

    private int towerCost;
    private int farmCost;
    private int warriorCost;
    private int civilianCost;
    private int hunterCost;
    private int siegeCost;
    private int bariccadeCost;
    private int plantationCost;
    public int collidersWithRef;

    public bool playerNotDead = true;
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
    public GameObject hooverObject;

    public Factions myFaction;
    private Faction faction;

    void Start()
    {
        
        //GetComponent<LineRenderer>().SetColors(GetComponent<PlayerColorManager>().myColor, GetComponent<PlayerColorManager>().myColor);
        faction = FactionHelper.GetFaction(myFaction);

        walkSpeed = faction.GetCursorSpeed();

        civilianCost = faction.GetCivilianCost();
        warriorCost = faction.GetWarriorCost();
        hunterCost = faction.GetHunterCost();
        siegeCost = faction.GetSiegeCost();

        farmCost = faction.GetFarmCost();
        plantationCost = faction.GetPlantationCost();
        bariccadeCost = faction.GetBarricadeCost();
        towerCost = faction.GetTowerCost();
    }

    void FixedUpdate()
    {
        if (hooverObject != null)
        {
            if (""+hooverObject.GetComponent<HooverData>().health != hooverUnitHealth.text)
            {
                FillHooverInfo();
            }
        }
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
        if ((Input.GetAxisRaw("Joy1_3thAxis") == 0 && playerID == 1) || (Input.GetAxisRaw("Joy2_3thAxis") == 0 && playerID == 2) && playerNotDead == true)
        {
            if (buildTriggerActivation == true)
            {
                if (buildMenuOptionSelection != -1)
                {

                    switch (buildMenuOptionSelection)
                    {
                        case 0:
                            farmCost = faction.GetFarmCost();
                            if (resourceManager.wood >= farmCost && collidersWithRef <= 0 && inBuildingBoundingBox == true)
                            {
                                AC_BuildObject.Play();
                                GameObject spawnFarmObj = Instantiate(farmObj, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;

                                spawnFarmObj.GetComponent<HealthManager>().healthTotal = faction.GetFarmHealth();

                                spawnFarmObj.GetComponent<ResourceSpawnManager>().growthTime = faction.GetFarmGrowthTime();
                                spawnFarmObj.GetComponent<ResourceSpawnManager>().resourceAmount = faction.GetFarmResourceAmount();
                                spawnFarmObj.GetComponent<ResourceSpawnManager>().resourceType = faction.GetFarmResourceType();

                                spawnFarmObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                spawnFarmObj.GetComponent<PlayerColorManager>().SetColor();
                                resourceManager.wood = resourceManager.wood - farmCost;
                                resourceManager.ChangeUI();
                            }
                            else
                            {
                                Debug.Log("farmcost: " + farmCost + ", collidersWithRef: " + collidersWithRef + ", inBuildingBoundingBox: " + inBuildingBoundingBox);
                                AC_CancelBuild.Play();
                            }
                            break;
                        case 1:
                            plantationCost = faction.GetPlantationCost();
                            if (resourceManager.wood >= plantationCost && collidersWithRef <= 0 && inBuildingBoundingBox == true)
                            {
                                AC_BuildObject.Play();
                                GameObject spawnPlantationObj = Instantiate(plantationObj, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;

                                spawnPlantationObj.GetComponent<HealthManager>().healthTotal = faction.GetPlantationHealth();

                                spawnPlantationObj.GetComponent<ResourceSpawnManager>().growthTime = faction.GetPlantationGrowthTime();
                                spawnPlantationObj.GetComponent<ResourceSpawnManager>().resourceAmount = faction.GetPlantationResourceAmount();
                                spawnPlantationObj.GetComponent<ResourceSpawnManager>().resourceType = faction.GetPlantationResourceType();

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
                            bariccadeCost = faction.GetBarricadeCost();
                            if (resourceManager.wood >= bariccadeCost && collidersWithRef <= 0 && inBuildingBoundingBox == true)
                            {
                                AC_BuildObject.Play();
                                GameObject spawnBarricadeObj = Instantiate(bariccadeObj, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;

                                spawnBarricadeObj.GetComponent<HealthManager>().healthTotal = faction.GetBarricadeHealth();

                                spawnBarricadeObj.GetComponent<PlayerColorManager>().playerID = playerID;
                                spawnBarricadeObj.GetComponent<PlayerColorManager>().SetColor();
                                resourceManager.wood = resourceManager.wood - bariccadeCost;
                                resourceManager.ChangeUI();
                            }
                            break;
                        case 3:
                            towerCost = faction.GetTowerCost();
                            if (resourceManager.wood >= towerCost && collidersWithRef <= 0 && inBuildingBoundingBox == true)
                            {
                                AC_BuildObject.Play();
                                GameObject spawnTowerObj = Instantiate(towerObj, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;

                                spawnTowerObj.GetComponent<HealthManager>().healthTotal = faction.GetTowerHealth();
                                spawnTowerObj.GetComponent<TowerManager>().range = faction.GetTowerRange();
                                spawnTowerObj.GetComponent<TowerManager>().fireRate = faction.GetTowerAttackSpeed();
                                spawnTowerObj.GetComponent<TowerManager>().towerAttack = faction.GetTowerAttack();

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
                            civilianCost = faction.GetCivilianCost();
                            if (resourceManager.food >= civilianCost && inUnitBoundingBox == true)
                            {
                                AC_TrainObject.Play();
                                GameObject spawnCivilianObj = Instantiate(civilianObj, unitSpawn.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnCivilianObj.GetComponent<HealthManager>().healthTotal = faction.GetCivilianHealth();
                                spawnCivilianObj.GetComponent<UnitController>().sight = faction.GetCivilianSight();
                                spawnCivilianObj.GetComponent<UnitController>().range = faction.GetCivilianRange();
                                spawnCivilianObj.GetComponent<UnitController>().speed = faction.GetCivilianMoveSpeed();
                                spawnCivilianObj.GetComponent<UnitController>().attack = faction.GetCivilianAttack();
                                spawnCivilianObj.GetComponent<UnitController>().attackSpeed = faction.GetCivilianAttackSpeed();

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
                            warriorCost = faction.GetWarriorCost();
                                if (resourceManager.food >= warriorCost && inUnitBoundingBox == true)
                                {
                                    AC_TrainObject.Play();
                                    GameObject spawnWarriorObj = Instantiate(warriorObj, unitSpawn.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnWarriorObj.GetComponent<HealthManager>().healthTotal = faction.GetWarriorHealth();
                                spawnWarriorObj.GetComponent<UnitController>().sight = faction.GetWarriorSight();
                                spawnWarriorObj.GetComponent<UnitController>().range = faction.GetWarriorRange();
                                spawnWarriorObj.GetComponent<UnitController>().speed = faction.GetWarriorMoveSpeed();
                                spawnWarriorObj.GetComponent<UnitController>().attack = faction.GetWarriorAttack();
                                spawnWarriorObj.GetComponent<UnitController>().attackSpeed = faction.GetWarriorAttackSpeed();

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
                            hunterCost = faction.GetHunterCost();
                            if (resourceManager.food >= hunterCost && inUnitBoundingBox == true)
                            {
                                AC_TrainObject.Play();
                                GameObject spawnHunterObj = Instantiate(hunterObj, unitSpawn.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnHunterObj.GetComponent<HealthManager>().healthTotal = faction.GetHunterHealth();
                                spawnHunterObj.GetComponent<UnitController>().sight = faction.GetHunterSight();
                                spawnHunterObj.GetComponent<UnitController>().range = faction.GetHunterRange();
                                spawnHunterObj.GetComponent<UnitController>().speed = faction.GetHunterMoveSpeed();
                                spawnHunterObj.GetComponent<UnitController>().attack = faction.GetHunterAttack();
                                spawnHunterObj.GetComponent<UnitController>().attackSpeed = faction.GetHunterAttackSpeed();

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
                            siegeCost = faction.GetSiegeCost();
                            if (resourceManager.food >= siegeCost && inUnitBoundingBox == true)
                            {
                                AC_TrainObject.Play();
                                GameObject spawnSiegeObj = Instantiate(siegeObj, unitSpawn.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                                spawnSiegeObj.GetComponent<HealthManager>().healthTotal = faction.GetSiegeHealth();
                                spawnSiegeObj.GetComponent<UnitController>().sight = faction.GetSiegeSight();
                                spawnSiegeObj.GetComponent<UnitController>().range = faction.GetSiegeRange();
                                spawnSiegeObj.GetComponent<UnitController>().speed = faction.GetSiegeMoveSpeed();
                                spawnSiegeObj.GetComponent<UnitController>().attack = faction.GetSiegeAttack();
                                spawnSiegeObj.GetComponent<UnitController>().attackSpeed = faction.GetSiegeAttackSpeed();

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
        if ((Input.GetAxisRaw("Joy1_3thAxis") < -sensitivity && buildTriggerActivation == false && playerID == 1) || (Input.GetAxisRaw("Joy2_3thAxis") < -sensitivity && buildTriggerActivation == false && playerID == 2))
        {
            BuildTrigger();
            buildTriggerActivation = true;
        }
        if ((Input.GetAxisRaw("Joy1_3thAxis") > sensitivity && trainTriggerActivation == false && playerID == 1) || (Input.GetAxisRaw("Joy2_3thAxis") > sensitivity && trainTriggerActivation == false && playerID == 2))
        {
            TrainTrigger();
            trainTriggerActivation = true;
        }
        if (buildTriggerActivation == true)
        {
            if ((Input.GetAxisRaw("Joy1_secondHorizontal") < -sensitivity && playerID == 1) || (Input.GetAxisRaw("Joy2_secondHorizontal") < -sensitivity && playerID == 2))
            {
                buildMenuOptionSelection = 3;
            }
            if ((Input.GetAxisRaw("Joy1_secondHorizontal") > sensitivity && playerID == 1) || (Input.GetAxisRaw("Joy2_secondHorizontal") > sensitivity && playerID == 2))
            {
                buildMenuOptionSelection = 1;
            }
            if ((Input.GetAxisRaw("Joy1_secondVertical") < -sensitivity && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") < -sensitivity && playerID == 2))
            {
                buildMenuOptionSelection = 0;
            }
            if ((Input.GetAxisRaw("Joy1_secondVertical") > sensitivity && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") > sensitivity && playerID == 2))
            {
                buildMenuOptionSelection = 2;
            }
            if((Input.GetAxisRaw("Joy1_secondVertical")  < sensitivity && Input.GetAxisRaw("Joy1_secondVertical") > -sensitivity && 
                Input.GetAxisRaw("Joy1_secondHorizontal")  < sensitivity && Input.GetAxisRaw("Joy1_secondHorizontal") > -sensitivity && playerID == 1) || 
                (Input.GetAxisRaw("Joy2_secondVertical") < sensitivity && (Input.GetAxisRaw("Joy2_secondVertical") > -sensitivity && 
                Input.GetAxisRaw("Joy2_secondHorizontal")  <sensitivity && (Input.GetAxisRaw("Joy2_secondHorizontal") >- sensitivity) && playerID == 2)))
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
            if ((Input.GetAxisRaw("Joy1_secondHorizontal") < -sensitivity && playerID == 1) || (Input.GetAxisRaw("Joy2_secondHorizontal") < -sensitivity && playerID == 2))
            {
                trainMenuOptionSelection = 3;
            }
            if ((Input.GetAxisRaw("Joy1_secondHorizontal") > sensitivity && playerID == 1) || (Input.GetAxisRaw("Joy2_secondHorizontal") > sensitivity && playerID == 2))
            {
                trainMenuOptionSelection = 1;
            }
            if ((Input.GetAxisRaw("Joy1_secondVertical") < -sensitivity && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") < -sensitivity && playerID == 2))
            {
                trainMenuOptionSelection = 0;
            }
            if ((Input.GetAxisRaw("Joy1_secondVertical") > sensitivity && playerID == 1) || (Input.GetAxisRaw("Joy2_secondVertical") > sensitivity && playerID == 2))
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
            if (hooveredUnit != null && hooveredUnit.GetComponent<UnitController>() != null && selectedUnit == null && hooveredUnit.GetComponent<PlayerColorManager>().playerID == playerID)
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
            hooverObject = col.gameObject;
            FillHooverInfo();
        }
    }
    public void FillHooverInfo()
    {
        hooverUnitName.text = "Name: " + hooverObject.gameObject.GetComponent<HooverData>().name.ToString();
        hooverUnitAvatar.enabled = true;
        hooverUnitAvatar.sprite = hooverObject.gameObject.GetComponent<HooverData>().avatar;
        hooverUnitAttack.text = "Attack: " + hooverObject.gameObject.GetComponent<HooverData>().attack.ToString();
        hooverUnitAttackSpeed.text = "AttackSpeed: " + hooverObject.gameObject.GetComponent<HooverData>().attackSpeed.ToString();
        hooverUnitMovementSpeed.text = "MovementSpeed: " + hooverObject.gameObject.GetComponent<HooverData>().movementSpeed.ToString();
        hooverUnitRange.text = "Range: " + hooverObject.gameObject.GetComponent<HooverData>().range.ToString();
        hooverUnitHealth.text = "Health: " + hooverObject.gameObject.GetComponent<HooverData>().health.ToString();
        hooveredUnit = hooverObject.gameObject;
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
            hooverUnitAvatar.enabled = false;
            hooverUnitAttack.text = "";
            hooverUnitAttackSpeed.text = "";
            hooverUnitMovementSpeed.text = "";
            hooverUnitRange.text = "";
            hooverUnitHealth.text = "";
            hooverObject = null;
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
            if(resourceTarget.GetComponent<ResourceSpawnManager>().resourceType == "wood" && resourceTarget.GetComponent<ResourceSpawnManager>().resourceActive == true)
            {
                resourceManager.wood = resourceManager.wood + resourceTarget.GetComponent<ResourceSpawnManager>().resourceAmount;
            }
            if (resourceTarget.GetComponent<ResourceSpawnManager>().resourceType == "food" && resourceTarget.GetComponent<ResourceSpawnManager>().resourceActive == true)
            {
                resourceManager.food = resourceManager.food + resourceTarget.GetComponent<ResourceSpawnManager>().resourceAmount;
            }
            AC_GrabResource.Play();
            resourceManager.ChangeUI();
            resourceTarget.GetComponent<ResourceSpawnManager>().ResourceReset();
        }
    }
}