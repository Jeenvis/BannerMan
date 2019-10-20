using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxManager : MonoBehaviour
{
    public string myBoundingBoxID;
    public int playerID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CursorController>() != null)
        {
            if (myBoundingBoxID == "unitBoundingBox")
            {
                other.GetComponent<CursorController>().inUnitBoundingBox = true;
            }
            if (myBoundingBoxID == "buildingBoundingBox")
            {
                other.GetComponent<CursorController>().inBuildingBoundingBox = true && other.GetComponent<CursorController>().playerID == playerID;
            }
            CursorChange(other);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CursorController>() != null)
        {
            if (myBoundingBoxID == "unitBoundingBox")
            {
                other.GetComponent<CursorController>().inUnitBoundingBox = false;
            }
            if (myBoundingBoxID == "buildingBoundingBox" && other.GetComponent<CursorController>().playerID == playerID)
            {
                other.GetComponent<CursorController>().inBuildingBoundingBox = false;
            }
            CursorChange(other);
        }
    }
    void CursorChange(Collider other)
    {
        if (other.GetComponent<CursorController>().inBuildingBoundingBox == true)
        {
            other.GetComponent<Renderer>().material = other.GetComponent<CursorController>().circleCursor;
        }
        else if (other.GetComponent<CursorController>().inUnitBoundingBox == true)
        {
            other.GetComponent<Renderer>().material = other.GetComponent<CursorController>().squareCursor;
        }
        else if(other.GetComponent<CursorController>().inBuildingBoundingBox == false && other.GetComponent<CursorController>().inUnitBoundingBox == false)
        {
            other.GetComponent<Renderer>().material = other.GetComponent<CursorController>().crossCursor;
        }
        other.GetComponent<PlayerColorManager>().SetColor(); 
    }
}
