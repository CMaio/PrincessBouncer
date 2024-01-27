using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float dashForce;
    public float dashSpeed;

    float dashing;
    Vector2 movement;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            dashing = dashForce;
        }
    }

    private void Move()
    {
        Vector2 currentPos = rigidBody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;
        movement = inputVector * (movementSpeed + dashing);
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime ;
        rigidBody.MovePosition(newPos);
        backToNormalState();
    }


    private void backToNormalState()
    {
        dashing -= dashing * dashSpeed * Time.fixedDeltaTime;
        
    }
}
