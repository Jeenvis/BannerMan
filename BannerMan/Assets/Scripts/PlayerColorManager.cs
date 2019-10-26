using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorManager : MonoBehaviour
{
    public GameObject myMaterial;
    public int playerID;
    public GameObject ColorManager;
    public Color myColor;

    // Start is called before the first frame update
    void Start()
    {
        if(myMaterial == null)
        {
            myMaterial = this.gameObject;
        }
        SetColor();
    }

    // Update is called once per frame
    public void SetColor()
    {
        ColorManager = GameObject.Find("ColorManager");
        if (ColorManager != null)
        {
            switch (playerID)
            {
                case 1:
                    myMaterial.GetComponent<Renderer>().material.SetColor("_Color", ColorManager.GetComponent<PlayerColorLibrary>().playerColor1);
                    break;
                case 2:
                    myMaterial.GetComponent<Renderer>().material.SetColor("_Color", ColorManager.GetComponent<PlayerColorLibrary>().playerColor2);
                    break;
                case 3:
                    myMaterial.GetComponent<Renderer>().material.SetColor("_Color", ColorManager.GetComponent<PlayerColorLibrary>().playerColor3);
                    break;
                case 4:
                    myMaterial.GetComponent<Renderer>().material.SetColor("_Color", ColorManager.GetComponent<PlayerColorLibrary>().playerColor4);
                    break;
                    default:
                    myMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    break;
            }
        }
        else
        {
            Debug.Log(this.name + " can't find the color manager");
        }
        myColor = myMaterial.GetComponent<Renderer>().material.color;
    }
}
