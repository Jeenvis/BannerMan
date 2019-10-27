using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorManager : MonoBehaviour
{
    public GameObject[] myMaterial;
    public int playerID;
    public GameObject ColorManager;
    public Color myColor;
    public bool textureMode = false;
    public bool colorMode = false;
    public Material[] colorMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if(myMaterial == null)
        {
            myMaterial[0] = this.gameObject;
        }
        SetColor();
    }

    // Update is called once per frame
    public void SetColor()
    {
            if (textureMode == true && myMaterial.Length > 0)
            {
            
                foreach (GameObject gO in myMaterial)
                {
                    if (gO.GetComponent<MeshRenderer>() != null)
                    {
                        gO.GetComponent<MeshRenderer>().material = colorMaterial[playerID - 1];
                    }
                    else if (gO.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        gO.GetComponent<SkinnedMeshRenderer>().material = colorMaterial[playerID - 1];
                    }
                }
            }
        }
}
