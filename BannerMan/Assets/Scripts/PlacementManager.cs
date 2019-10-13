using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int inCollider = 0;
    public Color baseColor;
    public CursorController myCursorController;
    // Start is called before the first frame update
    void Start()
    {
        baseColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "grabAbleResource" || other.tag == "targetObject")
        {
            inCollider = inCollider + 1;
            CheckPlacementPosibility();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "grabAbleResource" || other.tag == "targetObject")
        {
            inCollider = inCollider - 1;
            CheckPlacementPosibility();
        }
    }
    private void CheckPlacementPosibility()
    {
        myCursorController.collidersWithRef = inCollider;
        if (inCollider > 0)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else if(inCollider <= 0)
        {
            GetComponent<Renderer>().material.SetColor("_Color", baseColor);
        }
    }
}
