using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed parameters")]
    public float movementSpeed;
    public float dashForce;
    public float dashSpeed;

    [Header("Special components")]
    [SerializeField] InputController controls;

    
    float dashing;
    Vector2 movement;
    Vector2 mvControls;
    Rigidbody2D rigidBody;
    


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        controls = new InputController();
        controls.Player.Movement.performed += ctx => mvControls = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => mvControls = Vector2.zero;
        controls.Player.Dash.performed += ctx => dashing = dashForce;

    }

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
        Vector2 inputVector = new Vector2(mvControls.x, mvControls.y).normalized;
        movement = inputVector * (movementSpeed + dashing);
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime ;
        rigidBody.MovePosition(newPos);
        backToNormalState();
    }


    private void backToNormalState()
    {
        dashing -= dashing * dashSpeed * Time.fixedDeltaTime;

    }

    private void OnEnable(){controls.Enable();}
    private void OnDisable(){controls.Disable();}
}
