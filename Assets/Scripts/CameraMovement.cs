using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public Vector3 offset = new Vector3(0,0,0);
    public float smoothSpeed = 0;


    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 desiredPosition = player.position - offset;
        Vector2 smoothFollow = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothFollow;
    }
}
