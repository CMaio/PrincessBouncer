using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    InputController controls;



    void Awake()
    {
        controls = new InputController();
        controls.Player.Slap.performed += ctx => Slap();

    }


    void Slap()
    {

    }

}
