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


    InputController controls;
    CharacterRenderer characterRenderer;
    Rigidbody2D rigidBody;

    Vector2 movement;
    Vector2 mvControls;
    float dashing;
    float baseSpeed;

    int controlInvert = 1;
    


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        characterRenderer = GetComponentInChildren<CharacterRenderer>();
        controls = new InputController();
        SettingControllerOptiions();
        
        baseSpeed = movementSpeed;

    }

    void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        Vector2 currentPos = rigidBody.position;
        Vector2 inputVector = new Vector2(mvControls.x, mvControls.y).normalized;
        movement = (inputVector * controlInvert)  * (movementSpeed + dashing);
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        characterRenderer.SetDirection(movement);
        rigidBody.MovePosition(newPos);
        backToNormalState();
    }


    private void backToNormalState()
    {
        if(dashing < 0.001) { dashing = 0; }
        else if(dashing > 0.001) { dashing -= dashing * dashSpeed * Time.fixedDeltaTime; }
    }

    public void invertControls(bool invert)
    {
        controlInvert = invert ? -1 : 1;    
    }

    public void modifySpeed(float speed)
    {
        movementSpeed += speed;
    }

    public void ResetSpeed()
    {
        movementSpeed = baseSpeed;
    }

    private void OnEnable(){controls.Enable();}
    private void OnDisable(){controls.Disable();}

    private void SettingControllerOptiions()
    {
        controls.Player.Movement.performed += ctx => mvControls = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => mvControls = Vector2.zero;
        controls.Player.Dash.performed += ctx => dashing = dashForce;
    }
}
