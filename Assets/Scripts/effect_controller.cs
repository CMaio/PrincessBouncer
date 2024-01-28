using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_controller : MonoBehaviour
{
    [SerializeField] ParticleSystem dust_cloud;

    [SerializeField] Rigidbody2D Sheep;

    [SerializeField, Range(0,10)] private float cutoff_velocity = 1;

    float counter;

    // Update is called once per frame
    void Update()
    {

        if(Sheep.velocity.magnitude > cutoff_velocity)
        {
            dust_cloud.Play();
            
        }
        else 
        {
            dust_cloud.Stop();
        }

    }
}
