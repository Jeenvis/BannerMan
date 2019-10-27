using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovementManager : MonoBehaviour
{
    public int playerID;
    public Rigidbody rb;
    float curSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerID == 1)
        {
            rb.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal1") * curSpeed, 0.8f), 0, Mathf.Lerp(0, Input.GetAxis("Vertical1") * curSpeed, 0.8f));
        }
        if (playerID == 2)
        {
            rb.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal2") * curSpeed, 0.8f), 0, Mathf.Lerp(0, Input.GetAxis("Vertical2") * curSpeed, 0.8f));
        }
    }
}
