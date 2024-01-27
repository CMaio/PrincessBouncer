using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRenderer : MonoBehaviour
{
    public static readonly string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static readonly string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    public static readonly string attack = "Spining";
    public bool attacking = false;

    Animator animator;
    int lastDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;

        if(attacking) {
            animator.Play(attack);
        }
        else {

            if (direction.magnitude < .01f)
            {
                directionArray = staticDirections;

            }
            else
            {
                directionArray = runDirections;
                lastDirection = DirectionToIndex(direction, 8);
            }

            animator.Play(directionArray[lastDirection]);
        }
       
    }


    static int DirectionToIndex(Vector2 direction, int length)
    {
        Vector2 normDir = direction;

        float step = 360f / length;

        float halfStep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normDir);

        angle += halfStep;

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }
}
