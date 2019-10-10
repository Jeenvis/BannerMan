using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input Settings:")]
    public int playerID; //this can be made static serializeable

    [Space]
    [Header("Character Attributes:")]
    public float characterSpeed = 1;

    [Space]
    [Header("Character Statistics:")]
    public Vector2 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody rb;
    public GameObject visualRepresentation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Move();
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }
    void Move()
    {
        rb.velocity = new Vector3(movementDirection.x * movementSpeed, 0, movementDirection.y * movementSpeed) * characterSpeed;
    }
}
