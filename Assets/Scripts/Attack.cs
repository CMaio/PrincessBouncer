using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float timeOfAttack = 0;
    public float forceMoveEnemy = 0;

    float timeOfAttackRemaining = 0;
    float forceMovementBase = 0;
    bool attacking;
    InputController controls;
    CharacterRenderer characterRenderer;
    CircleCollider2D coliderAction;
    
    [SerializeField] ParticleSystem effect;


    void Awake()
    {
        characterRenderer = GetComponentInChildren<CharacterRenderer>();
        coliderAction = GetComponentInChildren<CircleCollider2D>();
        forceMovementBase = forceMoveEnemy;
        controls = new InputController();
        controls.Player.Slap.performed += ctx => Slap();

    }

    private void Update()
    {   
        if (timeOfAttackRemaining > 0)
        {
            timeOfAttackRemaining -= Time.deltaTime;
            if(timeOfAttackRemaining < 0.01)
            {
                timeOfAttackRemaining = 0;
                coliderAction.enabled = false;
                attacking = false;
                characterRenderer.attacking = attacking;
            }
        }
    }


    void Slap()
    {
        if (!attacking)
        {
            attacking = true;
            characterRenderer.attacking = attacking;
            coliderAction.enabled = true;
            timeOfAttackRemaining = timeOfAttack;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 dir = collision.GetContact(0).normal * -1;
        Rigidbody2D collisionGm = collision.gameObject.GetComponent<Rigidbody2D>();

        collisionGm.AddForce(dir * forceMoveEnemy,ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Collectable")
        {
            effect.Play();
        }
    }

    public void increaseAttack(int value) { forceMoveEnemy += value; }
    public void decreaseAttack() { forceMoveEnemy = forceMovementBase; }

    private void OnEnable() { controls.Enable(); }
    private void OnDisable() { controls.Disable(); }

}
