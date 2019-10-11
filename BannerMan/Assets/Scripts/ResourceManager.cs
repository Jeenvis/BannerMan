using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public int wood;
    public int food;

    public Text woodText;
    public Text foodText;
    // Start is called before the first frame update
    void Start()
    {
        ChangeUI();
        StartCoroutine(ResourceTrickle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeUI()
    {
        woodText.text = ("Wood: " + wood);
        foodText.text = ("Food: " + food);
    }
    IEnumerator ResourceTrickle()
    {
        yield return new WaitForSeconds(5);
        wood = wood + 1;
        food = food + 1;
        ChangeUI();
        StartCoroutine(ResourceTrickle());
    }
}
